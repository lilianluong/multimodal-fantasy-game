from character.character import Character
from trigger.trigger_recognizer import TriggerRecognizer

if __name__ == "__main__":
    player = Character("Player", 20, race = "Dragonborn", clss = "Bard")
    enemy = Character("Enemy", 10, race = "Human", clss = "Werewolf")
    trigger_recognizer = TriggerRecognizer()
    
    i = 1
    while True:
        print("It's your turn! Turn", i)
        result, score = trigger_recognizer.take_turn()
        if result is None:
            print("No spell detected")
        else:
            print("You cast:", result, "- score:", score)
        i += 1
