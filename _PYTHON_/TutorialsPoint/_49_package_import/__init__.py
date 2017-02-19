# These lines add package directory to search path...
from os import path as os_path
from sys import path as sys_path
dir_path = os_path.dirname(os_path.realpath(__file__))
sys_path.append(dir_path)

# ...then below modules can be imported
from Pots import Pots
from Isdn import Isdn
from G3 import G3

