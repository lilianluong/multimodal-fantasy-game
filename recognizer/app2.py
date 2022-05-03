from flask import Flask, jsonify, request
from trigger.trigger_recognizer import TriggerRecognizer
from shell.character.character import Character
from shell.character.spell import Spell
from shell.shell import PrintColors
import sys
import time

app = Flask(__name__)


class GlobalData:
    trigger_recognizer = None

    timeStartedAt = time.time()
    turnLength = 0
    turnFinished = False

    # set if a spell was cast
    spellCast = ""
    score = 1

    # always set if it matches
    spokenCommand = ""  # one of "move tutor", "adventure" or a spell name


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
    if not GlobalData.turnFinished:
        return jsonify(timeRemaining=max(time.time() - GlobalData.timeStartedAt, 0))
    else:
        return jsonify(
            timeRemaining=0,
            spellCast=GlobalData.spellCast,
            score=GlobalData.score,
            spokenCommand=GlobalData.spokenCommand,
        )


@app.route("/api/startTurn", methods=["POST"])
def post_start_turn():
    """
    Handles POST input with a turnLength float parameter and starts a new turn.
    If a turn is already going, ignore its result and start a new one.
    """
    turnLength = request.form["turnLength"]
    # TODO: run start_turn(turnLength) asynchronously and move to the return statement immediately
    return "started turn"


def start_system():
    """
    Set up the backend recognition system
    Start recording to handle non-turn voice commands
    """
    GlobalData.trigger_recognizer = TriggerRecognizer()


def start_turn(turn_length: float):
    """
    Set the number of seconds a turn should take and starts a new turn
    When results are obtained, save them in global variables for the endpoints to return
    """
    # start the turn
    t0 = time.time()
    GlobalData.timeStartedAt = t0
    GlobalData.turnLength = turn_length
    GlobalData.turnFinished = False

    result, score, speech = GlobalData.trigger_recognizer.take_turn(num_seconds=turn_length)

    if GlobalData.timeStartedAt != t0:
        # if a different process interrupted this just skip the rest
        return

    GlobalData.spellCast = result if result is not None else ""
    GlobalData.score = max(0, 1 - score) if score is not None else 0

    for option in ("move tutor", "adventure"):
        if option in speech:
            GlobalData.spokenCommand = option
            break
    GlobalData.turnFinished = True


if __name__ == "__main__":
    app.run(debug=True)
    start_system()
