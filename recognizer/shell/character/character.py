import random

class Character:
    def __init__(self, name, level, known_spells, race = "human"):
        """
        A character.
        """
        self._name = name
        self._race = race
        self._level = level
        self._hp = 100
        self._max_hp = 5
        
        self._strength = 0
        self._dexterity = 0
        self._constitution = 0
        self._intelligence = 0
        self._wisdom = 0
        self._charisma = 0
        
        self._shield = 0
        self._known_spells = known_spells
        
        self.set_attributes()
    
    def __str__(self):
        return self.name

    @property
    def name(self):
        return self._name

    @property
    def hp(self):
        return self._hp

    @property
    def max_hp(self):
        return self._max_hp

    @property
    def level(self):
        return self._level

    @property
    def shield(self):
        return self._shield

    @property
    def race(self):
        return self._race
    
    def damage(self, amt):
        self._hp -= int(amt - self._shield)
        self._hp = max(0, self._hp)
    
    def heal(self, amt):
        self._hp += int(amt)
        self._hp = min(self._max_hp, self._hp)
    
    def shield(self, amt):
        self._shield = amt
    
    def reset_shield(self,):
        self._shield = 0

    # Set character's attributes
    def set_attributes(self):
        if self.race == "human":
            self._strength = 1
            self._dexterity = 1
            self._constitution = 1
            self._intelligence = 1
            self._wisdom = 1
            self._charisma = 1
        elif self.race == "dragonborn":
            self._strength = 2
            self._charisma = 1
        elif self.race == "elf":
            self._dexterity = 2
            self._intelligence = 1
        elif self.race == "orc":
            self._strength = 2
            self._constitution = 1
        elif self.race == "werewolf":
            self._strength = 2
            self._constitution = 2
        elif self.race == "goblin":
            self._dexterity = 2
            self._constitution = 1
        else:
            self._race = "human"
            
        self._hp = self.level * (5 + self._constitution) * 20
        self._max_hp = self._hp
    
    def random_spell(self, enemy):
        spell = random.choice(self._known_spells)
        effects = self.spell_effects(enemy, 1, spell)
        return spell, effects
    
    def spell_effects(self, enemy, score, spell):
        heal_amt = spell.heal * self.level*score
        self.heal(heal_amt)
        damage_amt = spell.damage * self.level*score
        enemy.damage(damage_amt)
        enemy.reset_shield()
        shield_amt = spell.shield * self.level*score
        self.shield(shield_amt)
        effects = "Spell Effects: Heal " + str(heal_amt) + ", Damage " + str(damage_amt) + ", Shield " + str(shield_amt)
        return effects