from flask import Flask, jsonify, request

app = Flask(__name__)


class GlobalData:
    pass


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
    return jsonify()


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
    pass


def start_turn(turnLength: float):
    """
    Set the number of seconds a turn should take and starts a new turn
    When results are obtained, save them in global variables for the endpoints to return
    """
    pass


if __name__ == "__main__":
    app.run(debug=True)
    start_system()
