from trigger.trigger_database import TriggerDatabase

if __name__ == "__main__":
    db = TriggerDatabase()
    db.clear_data()
    db.add_trigger("flame", ["flame", "lame", "fame"], "horizontal_line")
    db.add_trigger("shield", ["shield"], "circle")
    db.add_trigger("cure", ["cure", "pure", "your", "you're"], "ex")
    db.save_data()