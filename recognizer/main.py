from trigger.trigger_recognizer import TriggerRecognizer
from shell.character.character import Character
from shell.character.spell import Spell
from shell.shell import PrintColors
import sys

def move_tutor(trigger_recognizer):
    PrintColors.print_header_row()
    # PrintColors.print_paragraph_text("Practice a move!")
    result, score, speech = trigger_recognizer.take_turn(use_gesture, use_speech, verbose = 0)
    if result is None:
        PrintColors.print_paragraph2_text("No spell detected. Audio: " + str(speech))
    else:
        PrintColors.print_paragraph2_text("SPELL CAST: " + str(result) + ", SCORE: " + str(score))
    
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
    PrintColors.print_subheader_row()
    PrintColors.print_subheader_text("In this game, you will battle enemies using your arsenal of spells.")
    PrintColors.print_subheader_text("Your player details are below, followed by your attributes. These attributes have")
    PrintColors.print_subheader_text("an impact on the effectiveness of your spells as well as your enemy's spells on you.")
    PrintColors.print_subheader_text("Begin by practicing some of your available spells.")
    PrintColors.print_subheader_row()
    PrintColors.print_subheader_text("At any point, say \"game tutorial\" to view these instructions,")
    PrintColors.print_subheader_text("\"move tutor\" to practice your moves, or \"end game\" to finish playing.")
    PrintColors.print_subheader_row()
    PrintColors.print_char_details(player)
    
    # Return to move tutor
    return "move_tutor"

def round(trigger_recognizer, player, enemy, i, spell_dict):
    if i == 1:
        round_one(trigger_recognizer, player, enemy)
        
    PrintColors.print_subheader_text("Round " + str(i))
    
    print_hp(player, enemy)
    
    PrintColors.print_header_row()
    # PrintColors.print_paragraph_text("Your turn!")
    result, score, speech = trigger_recognizer.take_turn(use_gesture, use_speech, verbose = 0)
    if result is None:
        PrintColors.print_paragraph2_text("No spell detected, try again. Audio: " + str(speech))
    else:
        PrintColors.print_paragraph2_text("SPELL CAST: " + str(result) + ", SCORE: " + str(max(0, 1 - score)))
        player_spell = spell_dict[result]
        player_spell_effects = player.spell_effects(enemy, max(0, 1 - score), player_spell)
        PrintColors.print_paragraph2_text(player_spell_effects)
        PrintColors.print_header_row()
        PrintColors.print_paragraph_text("Enemy turn!")
        enemy_spell, enemy_spell_effects = enemy.random_spell(player)
        PrintColors.print_paragraph2_text("SPELL CAST: " + str(enemy_spell) + ", SCORE: " + str(0.5))
        PrintColors.print_paragraph2_text(enemy_spell_effects)
        PrintColors.print_header_row()
    
    if enemy.hp == 0:
        PrintColors.print_subheader3_row()
        PrintColors.print_subheader3_text("You win!")
        PrintColors.print_subheader3_row()
        state = "end_game"
    elif player.hp == 0:
        PrintColors.print_subheader3_row()
        PrintColors.print_subheader3_text("You lose.")
        PrintColors.print_subheader3_row()
        state = "end_game"
    
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

def round_one(trigger_recognizer, player, enemy):
    PrintColors.print_header_row()
    PrintColors.print_subheader3_row()
    PrintColors.print_subheader3_text("A new game is beginning!")
    PrintColors.print_subheader3_text("An enemy approaches!")
    PrintColors.print_subheader3_row()
    if enemy.race == "werewolf":
        with open("werewolf.txt", "rb") as f:
            for line in f:
                PrintColors.print_paragraph2_text(str(line.rstrip().decode("utf-8")))
    PrintColors.print_header_row()
    PrintColors.print_header_row()

def print_hp(player, enemy):
    # PrintColors.print_subheader_row()
    enemy_bar = int(50*enemy.hp/enemy.max_hp)
    player_bar = int(50*player.hp/player.max_hp) #▥
    PrintColors.print_red_text("|"*enemy_bar + " "*(50 - enemy_bar) + "|  " +
                    str(enemy.name) + " HP: " + str(enemy.hp) + "/" + str(enemy.max_hp) +
                    ", SHIELD: " + str(enemy._shield))
    PrintColors.print_green_text("|"*player_bar + " "*(50 - player_bar) + "|  " +
                    str(player.name) + " HP: " + str(player.hp) + "/" + str(player.max_hp) +
                    ", SHIELD: " + str(player._shield))

if __name__ == "__main__":
    PrintColors.print_test()
    argumentList = sys.argv[1:]  # + ["--no_gesture"]
    use_gesture = False if "--no_gesture" in argumentList else True
    use_speech = False if "--no_speech" in argumentList else True
    
    attack = Spell("attack", 10, 0, 0)
    heal = Spell("heal", 0, 30, 0)
    shield = Spell("shield", 0, 0, 20)
    spell_dict = {"attack": attack, "heal": heal, "shield": shield}
    
    trigger_recognizer = TriggerRecognizer(use_gesture, use_speech)
    i = 1
    state = "game_tutorial" # "round"
    player = Character("Player", 1, [attack, heal, shield])
    enemy = Character("Enemy", 1, [attack, heal, shield], race="werewolf")
    
    PrintColors.print_header_row()
    PrintColors.print_header_text("~~~ Welcome to the Multimodal Adventure Fantasy Game! ~~~")
    PrintColors.print_header_row()
    
    while True:
        if state == "move_tutor":
            state = move_tutor(trigger_recognizer)
        elif state == "game_tutorial":
            state = game_tutorial(player)
        elif state == "round":
            state = round(trigger_recognizer, player, enemy, i, spell_dict)
            i += 1
        elif state == "end_game":
            print("Game ended.")
