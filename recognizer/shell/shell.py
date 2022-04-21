from shell.character.character import Character

class PrintColors:
    CEND      = '\33[0m'
    CBOLD     = '\33[1m'
    CITALIC   = '\33[3m'
    CURL      = '\33[4m'
    CSELECTED = '\33[7m'

    CBLACK  = '\33[30m'
    CRED    = '\33[31m'
    CGREEN  = '\33[32m'
    CYELLOW = '\33[33m'
    CBLUE   = '\33[34m'
    CVIOLET = '\33[35m'
    CBEIGE  = '\33[36m'
    CWHITE  = '\33[37m'

    CBLACKBG  = '\33[40m'
    CREDBG    = '\33[41m'
    CGREENBG  = '\33[42m'
    CYELLOWBG = '\33[43m'
    CBLUEBG   = '\33[44m'
    CVIOLETBG = '\33[45m'
    CBEIGEBG  = '\33[46m'
    CWHITEBG  = '\33[47m'

    CGREY    = '\33[90m'
    CRED2    = '\33[91m'
    CGREEN2  = '\33[92m'
    CYELLOW2 = '\33[93m'
    CBLUE2   = '\33[94m'
    CVIOLET2 = '\33[95m'
    CBEIGE2  = '\33[96m'
    CWHITE2  = '\33[97m'

    CGREYBG    = '\33[100m'
    CREDBG2    = '\33[101m'
    CGREENBG2  = '\33[102m'
    CYELLOWBG2 = '\33[103m'
    CBLUEBG2   = '\33[104m'
    CVIOLETBG2 = '\33[105m'
    CBEIGEBG2  = '\33[106m'
    CWHITEBG2  = '\33[107m'
    
    def print_test():
        x = 0
        for i in range(24):
            colors = ""
            for j in range(5):
                code = str(x+j)
                colors = colors + "\33[" + code + "m\\33[" + code + "m\033[0m "
            print(colors)
            x = x + 5
            
    def print_table():
        """
        prints table of formatted text format options
        """
        for style in range(8):
            for fg in range(30,38):
                s1 = ''
                for bg in range(40,48):
                    format = ';'.join([str(style), str(fg), str(bg)])
                    s1 += '\x1b[%sm %s \x1b[0m' % (format, format)
                print(s1)
            print('\n')
    
    def print_header_row():
        print(PrintColors.CWHITE2 + PrintColors.CBLACKBG + ' '*100 + PrintColors.CEND)
    
    def print_header_text(text):
        pad = int((100 - len(text))/2)
        print(PrintColors.CWHITE2 + PrintColors.CBLACKBG + ' '*(pad) + text + ' '*(100-pad-len(text)) + PrintColors.CEND)
    
    def print_subheader_row():
        print(PrintColors.CWHITE2 + PrintColors.CWHITEBG + ' '*100 + PrintColors.CEND)

    def print_subheader_text(text):
        pad = int((100 - len(text))/2)
        print(PrintColors.CWHITE2 + PrintColors.CWHITEBG + ' '*(pad) + text + ' '*(100-pad-len(text)) + PrintColors.CEND)
    
    def print_subheader2_text(text):
        pad = int((100 - len(text))/2)
        print(PrintColors.CBLACK + PrintColors.CBOLD + PrintColors.CWHITEBG + ' '*(pad) + text + ' '*(100-pad-len(text)) + PrintColors.CEND)
    
    def print_subheader3_row():
        print(PrintColors.CWHITE2 + PrintColors.CBLUEBG2 + ' '*100 + PrintColors.CEND)
    
    def print_subheader3_text(text):
        pad = int((100 - len(text))/2)
        print(PrintColors.CWHITE2 + PrintColors.CBOLD + PrintColors.CBLUEBG2 + ' '*(pad) + text + ' '*(100-pad-len(text)) + PrintColors.CEND)
    
    def print_paragraph_text(text):
        print(PrintColors.CBLUE + PrintColors.CBOLD + PrintColors.CBLACKBG + ' '*(3) + text + ' '*(100-3-len(text)) + PrintColors.CEND)
    
    def print_paragraph2_text(text):
        print(PrintColors.CWHITE2 + PrintColors.CBLACKBG + ' '*(3) + text + ' '*(100-3-len(text)) + PrintColors.CEND)
        
    def print_red_text(text):
        print(PrintColors.CRED + PrintColors.CBLACKBG + ' '*(3) + text + ' '*(100-3-len(text)) + PrintColors.CEND)
        
    def print_green_text(text):
        print(PrintColors.CGREEN2 + PrintColors.CBLACKBG + ' '*(3) + text + ' '*(100-3-len(text)) + PrintColors.CEND)
        
    def print_char_details(character):
        basic_details = str("NAME: " + character.name + 
              ", HP: " + str(character.hp) +
              ", LEVEL: " + str(character.level) +
              ", RACE: " + character.race)
        attribute_details = str("STR: " + str(character._strength) + 
              ", DEX: " + str(character._dexterity) +
              ", CON: " + str(character._constitution) +
              ", INT: " + str(character._intelligence) +
              ", WID: " + str(character._wisdom) +
              ", CHA: " + str(character._charisma))
        spells_details = str("SPELLS: " + ", ".join([str(s) for s in character._known_spells]))
        print(PrintColors.CWHITE2 + PrintColors.CBOLD + PrintColors.CBLUEBG2 +
              ' '*(3) + basic_details + ' '*(97 - len(basic_details)) +
              PrintColors.CEND)
        print(PrintColors.CWHITE2 + PrintColors.CBLUEBG2 + 
              ' '*(3) + attribute_details + ' '*(97 - len(attribute_details)) +
              PrintColors.CEND)
        print(PrintColors.CWHITE2 + PrintColors.CBLUEBG2 + 
              ' '*(3) + spells_details + ' '*(97 - len(spells_details)) +
              PrintColors.CEND)
    
    
        
    