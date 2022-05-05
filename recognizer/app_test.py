from flask import Flask, jsonify, request
from trigger.trigger_recognizer import TriggerRecognizer
import sys
import time
from multiprocessing import Process, Value

app = Flask(__name__)

SPOKEN_COMMANDS = ["spell tutor", "adventure", "tutorial", "flame", "shield", "cure", "lightning", "leech", "hide"]


GlobalSharedData = (
    Value('i', 0),  # needToStartTurn

    Value('d', time.time()),  # timeStartedAt
    Value('d', 2.5),  # turnLength
    Value('i', 1),  # turnFinished

    Value('i', -1),  # spellCast
    Value('d', 0),  # score

    Value('i', -1),  # spokenCommand
)


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
    if GlobalSharedData[3].value == 1:  # turnFinished is 0
        current_time = time.time() - GlobalSharedData[1].value
        return jsonify(timeRemaining=max(GlobalSharedData[2].value - current_time, 0), turnState=1)
    else:
        spell_cast_index = GlobalSharedData[4].value
        spoken_command_index = GlobalSharedData[6].value
        return jsonify(
            timeRemaining=-1,
            turnState=2,
            spellCast="" if spell_cast_index == -1 else SPOKEN_COMMANDS[spell_cast_index],
            score=GlobalSharedData[5].value,
            spokenCommand="" if spoken_command_index == -1 else SPOKEN_COMMANDS[spoken_command_index],
        )


@app.route("/api/startTurn", methods=["POST"])
def post_start_turn():
    """
    Handles POST input with a turnLength float parameter and starts a new turn.
    If a turn is already going, return 0 else return 1
    """
    turn_length = float(request.form["turnLength"])
    if GlobalSharedData[0].value == 1:
        # already going
        return "0"
    GlobalSharedData[2].value = turn_length  # turnLength
    GlobalSharedData[0].value = 1  # needToStartTurn
    return "1"


@app.route("/api/putInInput", methods=["GET"])
def get_test_input():
    result = request.args["spellCast"]
    result_score = float(request.args["score"])
    speech = request.args["speech"]

    GlobalSharedData[4].value = SPOKEN_COMMANDS.index(result) if result is not None and result != "" else -1
    GlobalSharedData[5].value = max(0.0, 1 - result_score) if result_score is not None else 0

    spoken_command_index = -1
    for i, option in enumerate(SPOKEN_COMMANDS):
        if option in speech:
            spoken_command_index = i
            break
    GlobalSharedData[6].value = spoken_command_index
    GlobalSharedData[3].value = 2
    return f"{result}, {result_score}, {speech}"


def start_system(
        needToStartTurn,
        timeStartedAt,
        turnLength,
        turnFinished,
        spellCast,
        score,
        spokenCommand
        ):
    """
    Set up the backend recognition system
    """
    print("Starting system...")
    while True:
        if needToStartTurn.value == 1:
            print(f"Start turn for {turnLength.value} seconds.")
            t0 = time.time()
            timeStartedAt.value = t0
            turnFinished.value = 1

            while turnFinished.value == 1:
                time.sleep(0.02)
            # TODO: how to handle interruptions? assume it won't happen?

            needToStartTurn.value = 0


def main():
    p1 = Process(target=start_system, args=GlobalSharedData)
    p1.start()
    app.run(debug=True)


if __name__ == "__main__":
    main()
