# Multimodal Fantasy Game
6.835 Final Project

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
- .gitignore
- LICENSE
- README.md
