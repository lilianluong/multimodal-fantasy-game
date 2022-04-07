from trigger.trigger_database import TriggerDatabase

if __name__ == "__main__":
    db = TriggerDatabase()
    db.clear_data()
    db.add_trigger("attack", ["attack"], "horizontal_line")
    db.add_trigger("shield", ["shield"], "circle")
    db.add_trigger("heal", ["heal", "he'll", "eel"], "ex")
    db.save_data()
