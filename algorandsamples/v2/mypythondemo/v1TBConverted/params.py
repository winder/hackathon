from os.path import expanduser
home = expanduser("~")

# change these after starting the node and kmd
# algod info is in the algod.net and algod.token files in the data directory
# kmd info is in the kmd.net and kmd.token files in the kmd directory in data
# change these after starting the node and kmd

kmd_token = "your kmd_token"
kmd_address = "http://localhost:7833"

#     Algorand Hackathon
algod_token = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1"
algod_address = "http://hackathon.algodev.network:9100"

#     your own node
# algod_address = "http://localhost:8080"
# algod_token = "your ALGOD_API_TOKEN"

#  Purestake
# algod_address = "https://testnet-algorand.api.purestake.io/ps1"
# algod_token = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab"

# path to the data directory
# data_dir_path = home + "/node/data"
# kmd_folder_name = "kmd-v0.5"  # name of the kmd folder in the data directory

# get tokens and addresses automatically, if data_dir_path is not empty
# if data_dir_path and kmd_folder_name:
#     if not data_dir_path[-1] == "/":
#         data_dir_path += "/"
#     if not kmd_folder_name[-1] == "/":
#         kmd_folder_name += "/"
# algod_token = open(data_dir_path + "algod.token", "r").read().strip("\n")
# algod_address = "http://" + open(data_dir_path + "algod.net",
#                                  "r").read().strip("\n")
# algod_address = "http://" + open("/node/data" + "algod.net",
#                                  "r").read().strip("\n")

# kmd_token = open(data_dir_path + kmd_folder_name + "kmd.token",
#                  "r").read().strip("\n")
# kmd_address = "http://" + open(data_dir_path + kmd_folder_name + "kmd.net",
#                                "r").read().strip("\n")
