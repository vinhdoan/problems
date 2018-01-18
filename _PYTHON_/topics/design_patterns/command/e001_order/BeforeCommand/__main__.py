import sys
from command_executor import CommandExecutor


if len(sys.argv) < 2:
    print("Usage: python3 -m e001_order <command>")
    print("Commands:")
    print("\t\tCreateOrder")
    print("\t\tUpdateQuantity number")
    print("\t\tShipOrder")
    exit()

executor = CommandExecutor()
executor.execute_command(sys.argv[1:])
