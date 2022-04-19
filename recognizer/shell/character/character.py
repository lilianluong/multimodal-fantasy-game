

class Character:
    def __init__(self, name, hp, race = "", clss = "", level = 1):
        """
        A character.
        """
        self._name = name
        self._hp = hp
        self._race = race
        self._clss = clss
        self._level = level
        
        self._strength = 0
        self._dexterity = 0
        self._constitution = 0
        self._intelligence = 0
        self._wisdom = 0
        self._charisma = 0
        
        self._shielded = False

    @property
    def name(self):
        return self.name

    @property
    def hp(self):
        return self.hp

    @property
    def shielded(self):
        return self.shielded
    
    def damage(self, roll):
        if self.shielded(): self.hp -= roll/2
        else: self.hp -= roll
    
    def shield_on(self):
        self.shielded = True
    
    def shield_off(self):
        self.shielded = False
    
    def heal(self):
        self.hp += 5

    # Set character's attributes
    # TODO: Streamline in the future
    def set_attributes(self, strength, dexterity, constitution, intelligence, wisdom, charisma):
        self.strength = strength
        self.dexterity = dexterity
        self.constitution = constitution
        self.intelligence = intelligence
        self.wisdom = wisdom
        self.charisma = charisma