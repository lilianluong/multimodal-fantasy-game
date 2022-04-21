

class Spell:
    def __init__(self, name, damage, heal, shield):
        """
        A spell.
        """
        self._name = name
        self._damage = damage
        self._heal = heal
        self._shield = shield
    
    def __str__(self):
        return self.name

    @property
    def name(self):
        return self._name

    @property
    def damage(self):
        return self._damage

    @property
    def heal(self):
        return self._heal

    @property
    def shield(self):
        return self._shield