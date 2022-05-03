from flask import Flask, jsonify, request
from trigger.trigger_recognizer import TriggerRecognizer
from shell.character.character import Character
from shell.character.spell import Spell
from shell.shell import PrintColors
import sys

app = Flask(__name__)


class GlobalData:
    attack = Spell("attack", 10, 0, 0)
    heal = Spell("heal", 0, 30, 0)
    shield = Spell("shield", 0, 0, 20)
    spell_dict = {"attack": attack, "heal": heal, "shield": shield}
    
    state = 'game_tutorial'
    player = Character("Player", 1, [attack, heal, shield])
    enemy = Character("Enemy", 1, [attack, heal, shield], race="werewolf")
    i = 1
    
    player_spell = None
    player_score = 0
    enemy_spell = None
    enemy_score = 0.5
    
    def __str__(self):
        return GlobalData.state
    
    def json():
        return {"state": GlobalData.state,
                "i": GlobalData.i,
                "player": GlobalData.player,
                "player_spell": GlobalData.player_spell,
                "player_score": GlobalData.player_score,
                "enemy": GlobalData.enemy,
                "enemy_spell": GlobalData.enemy_spell,
                "enemy_score": GlobalData.enemy_score}
    
    def move_tutor(trigger_recognizer, spell_dict):
        result, score, speech = trigger_recognizer.take_turn()
        if result:
            GlobalData.player_spell = spell_dict[result]
            GlobalData.player_score = score
        else:
            GlobalData.player_spell = None
            GlobalData.player_score = 0
        
        # Handle state switches
        if speech is None:
            return "move_tutor"
        elif "begin" in speech.lower():
            return "round"
        elif "tutorial" in speech.lower():
            return "game_tutorial"
        elif "end" in speech.lower():
            return "end_game"
        else:
            return "move_tutor"
        
    def game_tutorial(player):
        # Return to move tutor
        return "move_tutor"
    
    def round(trigger_recognizer, player, enemy, i, spell_dict):
        result, score, speech = trigger_recognizer.take_turn()
        if result:
            GlobalData.player_spell = spell_dict[result]
            GlobalData.player_score = score
            player_spell_effects = player.spell_effects(enemy, max(0, 1 - score), GlobalData.player_spell)
            GlobalData.enemy_spell, enemy_spell_effects = enemy.random_spell(player)
        else:
            GlobalData.player_spell = None
            GlobalData.player_score = 0
            GlobalData.enemy_spell = None
        
        # Handle winning or losing
        if enemy.hp == 0:
            return "end_game"
        elif player.hp == 0:
            return "end_game"
        
        # Handle state switches
        if speech is None:
            return "round"
        elif "tutor" in speech.lower():
            return "move_tutor"
        elif "tutorial" in speech.lower():
            return "game_tutorial"
        elif "end" in speech.lower():
            return "end_game"
        else:
            return "round"


@app.route("/")
def hello_world():
    return "<p>Hello, world!</p>"


@app.route("/api/pollTurn")
def get_poll_turn():
    """
    Return turn state, and if it has finished, start a new turn.
    :return: necessary turn information as a JSON following the format below:
    {
        timeRemaining: [float, None],  // return None when turn has ended
        spellCast: [string, None],  // return None if no spell was returned
        score: [float, None],
        ...
    }
    """
    return jsonify(GlobalData.json())


@app.route("/api/startTurn", methods=["POST"])
def post_start_turn():
    """
    Handles POST input with a turnLength float parameter and starts a new turn.
    If a turn is already going, ignore its result and start a new one.
    """
    turnLength = request.form["turnLength"]
    return "started turn"


def start_system():
    """
    Set up the backend recognition system
    Start recording to handle non-turn voice commands
    """
    GlobalData.trigger_recognizer = TriggerRecognizer()


def start_turn(turnLength: float):
    """
    Set the number of seconds a turn should take and starts a new turn
    When results are obtained, save them in global variables for the endpoints to return
    """
    if GlobalData.state == "move_tutor":
        GlobalData.state = GlobalData.move_tutor(GlobalData.trigger_recognizer, GlobalData.spell_dict)
    elif GlobalData.state == "game_tutorial":
        GlobalData.state = GlobalData.game_tutorial(GlobalData.player)
    elif GlobalData.state == "round":
        GlobalData.state = round(GlobalData.trigger_recognizer, GlobalData.player, GlobalData.enemy, GlobalData.i, GlobalData.spell_dict)
        GlobalData.i += 1
    elif GlobalData.state == "end_game":
        print("Game ended.")


if __name__ == "__main__":
    app.run(debug=True)
    start_system()
