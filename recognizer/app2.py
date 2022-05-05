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
    Value('i', 0),  # turnFinished

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
        turnState: int,  // 0 for waiting, 1 for started recording, 2 for done
        spellCast: [string, None],  // return None if no spell was returned
        score: [float, None],
        ...
    }
    """
    if GlobalSharedData[3].value == 0:  # turnFinished is 0
        return jsonify(turnState=0)
    elif GlobalSharedData[3].value == 1:  # turnFinished is 1
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
    use_speech = True
    use_gesture = True
    trigger_recognizer = TriggerRecognizer(use_gesture=use_gesture, use_speech=use_speech)
    print("Starting system...")
    while True:
        if needToStartTurn.value == 1:
            print(f"Start turn for {turnLength.value} seconds.")
            turnFinished.value = 0

            result, result_score, speech = trigger_recognizer.take_turn(num_seconds=turnLength.value,
                                                                        use_gesture=use_gesture, use_speech=use_speech,
                                                                        global_args=(timeStartedAt, turnFinished))

            # TODO: how to handle interruptions? assume it won't happen?

            spellCast.value = SPOKEN_COMMANDS.index(result) if result is not None else -1
            score.value = max(0.0, 1 - result_score) if result_score is not None else 0

            spoken_command_index = -1
            for i, option in enumerate(SPOKEN_COMMANDS):
                if option in speech:
                    spoken_command_index = i
                    break
            spokenCommand.value = spoken_command_index
            turnFinished.value = 2
            needToStartTurn.value = 0


def main():
    p1 = Process(target=start_system, args=GlobalSharedData)
    p1.start()
    app.run(debug=True)


if __name__ == "__main__":
    main()
