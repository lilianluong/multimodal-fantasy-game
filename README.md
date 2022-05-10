# Multimodal Fantasy Game
6.835 Final Project

## Setup and Running Instructions

**Requirements:**
 - Windows 10
 - Python 3.8.0 32-bit
 - Leap Motion Controller
 - Leap Motion Tracking Software Windows 2.3.1 (https://developer.leapmotion.com/releases)
 - Microphone input
 
**Python Installations**

Replace `pip install ...` and `python ...` commands in the below instructions depending on your installation as necessary.
 1. Install PyAudio:
    - `pip install pipwin` and then `pipwin install PyAudio`
    - If the above commands don't work, try other solutions: https://stackoverflow.com/questions/52283840/i-cant-install-pyaudio-on-windows-how-to-solve-error-microsoft-visual-c-14
 2. Install other requirements from requirements.txt: `pip install -r requirements.txt`
 
**Starting the Game**
 1. Plug in Leap Motion Controller (make sure it works using the Leap visualizer)
    - Make sure in the Leap Motion Control Panel, "Allow Background Apps" is checked.
 2. Start `app.py` from the `recognizer/` directory:
    - `cd recognizer` and then `python app.py`
 3. Make sure the Flask app is running at http://127.0.0.1:5000/ (which should just print "Hello, world!")
    - Make sure that "Recorder ready" is printed in the terminal before proceeding
    (if you do not reach this point, check the Troubleshooting section below)
 4. Navigate to `game/Builds/` (i.e. using File Explorer) and run `Kalpana.exe`
 5. To quit the game, press Esc twice at any point in-game.
 
**Playing the Game**

Instructions are provided on the HOW TO PLAY screen in-game, but here are a few additional notes:
 - This is a game where you learn and practice spells. Say "Spell Tutor" to access a screen where you can switch between
 different spells and practice casting them. Then say "Adventure" to switch to a turn-based combat game, or "Tutorial"
 to return to the written instructions.
 - To cast spells, say the name of the spell while making the gesture with one hand over the Leap Motion Controller.
   - Make sure you're speaking and performing the gesture **when the turn timer starts and before it hits 0 seconds**.
   - Switching between screens using speech commands should also be done before the timer hits 0
   - **Keep your hand open and palm facing down** over the Leap Motion Controller for the best results

#### Troubleshooting

**When I start `app.py`, nothing is printed out in the terminal!**

The app is unable to connect to the Leap Motion Controller. Make sure it is connected and check the settings in the
Leap Motion Control Panel (e.g. enable "Allow Background Apps"). "Gesture recorder connected!" will be printed out when
the connection is detected.

**When I start `app.py`, I encounter a Leap import error.**

There is no official Leap Motion SDK for Python 3, though there are instructions to build a Python wrapper. I built one
for 32-bit Python 3.8.0, which is what we used to develop this system. If it doesn't work for you running the same version
of Python on Windows 10, try building a wrapper yourself (instructions can be Google'd) or use one made by someone else (e.g. for
Python 3.8 x64, try this: https://github.com/ano0002/Leap-Motion-Python-3.8).

**I don't see a timer in the game, or the timer is stuck at the same number.**

The game isn't communicating properly with the server. Make sure the server is running at http://127.0.0.1:5000/. Try
closing the game and the Flask app, then run the Flask app, make sure "Recorder ready" is printed in the terminal,
and **then** start the game executable again.

**The game won't detect that I casted a spell.**

Make sure that you are saying the name of the spell while performing the gesture, and completing both the phrase and gesture
before the on-screen timer hits 0. Players often perform the gesture too quickly - follow the timing of the gesture in the
spell tutor. Make sure you keep your hand open and palm face-down to make sure the Leap Motion Controller can detect your hand.
If the spell log still doesn't display a spell, alt-tab to the Python terminal where `app.py` is running and make sure
what you are saying is clearly detected.

**My spells are being detected with very low accuracy.**

Make sure your gestures are performed before the timer hits 0, and that your hand is open and your palm faces downwards.
Keep your hand in the space above the Leap Motion Controller and make gestures aligned vertically and horizontally along
the length of the Leap Motion Controller.

## Contents

There are many files in this repository that are necessary, for example to load the Unity project, or are included as
utility scripts for testing/evaluation, but aren't relevant to running the system. To be concise, the following list
will only cover important and/or relevant files and directories for understanding the codebase and running the
application.

- game/ - _source code of Unity game and interface_
  - Assets/
    - Art/ - _title and background images used in the game_
    - Audio/bgm.mp3 - _background music file_
    - JsonDotNet/ - _third party Unity library for handling JSON_
    - Prefabs/ - _Unity object templates_
    - Resources/ - _creature image assets_
    - Scenes/ - _Unity scene files for different screens of game_
    - Scripts/
      - Adventure/
        - Character/
          - Character.cs - _abstract class definition for character description_
          - CharacterState.cs - _class that contains state of each character (e.g. health)_
          - Enemies.cs - _implementation of enemy Character classes_
          - Player.cs - _implementation of player Character class_
        - UI/
          - AdventureUIController.cs - _main UI control functions_
          - ColorFlareScript.cs - _color effect when a spell is cast_
          - HealthBar.cs - _player/enemy health bars_
          - NotifierScript.cs - _text that pops up describing what happens_
        - AdventureController.cs - _main controller script for Adventure scene gameplay_
      - Misc/
        - BGMScript.cs - _plays background music_
        - QuitScript.cs - _allows player to quit game by pressing esc twice_
        - TitleMenuScript.cs - _transitions from title to instructions after x seconds_
        - TutorialController.cs - _main controller script for Tutorial screen_
      - Server/
        - ServerManager.cs - _functions to make HTTP requests to Python backend server_
        - ServerUtils.cs - _utility definitions for server requests_
      - Tutor/ - _scripts for Spell Tutor scene_
        - SpellExamples.cs - _templates for showing spell gestures_
        - SpellNameButtonScript.cs - _buttons that can be clicked to switch spells_
        - TutorAnimator.cs - _animated gesture dot_
        - TutorController.cs - _main controller script for Spell Tutor scene_
        - TutorUIController.cs - _functions managing UI of spell tutor_
      - GlobalData.cs - _static data persisting between scenes_
      - Utils.cs - _misc utility classes/enums used throughout_
  - **Builds/win/Kalpana.exe** - _built executable for game program`
  - Packages/ - _descriptors of packages used in Unity project_
  - ProjectSettings/ - _general settings for Unity project_
- recognizer/ - _source code of Python spell recognition backend_
  - gesture/
    - gesture_recognizer.py - _gesture recognition functions_
    - gesture_test.py - _scripts for testing gesture scripts_
    - gesture_types.py - _class definitions for Gesture and GestureDatabase_
    - gesture_util.py - _normalizing gesture script_
    - hardcoded_gestures.py - _initializes GestureDatabase with spell gestures_
    - template_matching.py - _template matching algorithm based on MP1_
  - recorders/
    - leap/ - _Leap SDK set up for Python 3.8_
    - gesture_recorder.py - _class that initializes Leap Motion Controller and processes input from it_
    - manual_leap_tester.py - _tests Leap input, used to record spell gestures for database_
  - shell/ - _scripts for testing system using terminal interface_
  - speech/
    - speech_recorder.py - _functions for speech input and processing_
    - speech_test.py - _test script for speech recorder_
  - trigger/
    - init_trigger_database.py - _initializes TriggerDatabase with spell data_
    - trigger_database.py - _defines TriggerDatabase class_
    - trigger_recognizer.py - _class that manages spell recognition loop and integrates gesture/speech recognition_
  - unity_import_utils/tutor_example_transfer.py - _loads data and converts into C# to be copied to Unity_
  - **app.py** - _main Flask app and API endpoints_
  - config.py - _global config variables_
  - gesture_data.pkl - _pickled database of gesture templates_
  - terminal.py - _test scripts through terminal_
  - trigger_data.pkl - _pickled database of spells_
- requirements.txt - _Python requirements_
