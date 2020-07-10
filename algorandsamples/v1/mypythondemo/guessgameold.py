import random
import sys
import time
from algosdk import transaction
from algosdk import account, algod, mnemonic
# from sendtransaction import defaultAddr, private_key_alc



# Setup HTTP client w/guest key provided by PureStake


class Connect():
    def __init__(self):
        # declaring the third party API
        # self.algod_address = "https://testnet-algorand.api.purestake.io/ps1"
        # # <-----shortened - my personal API token
        # self.algod_token = "eVXi2wPlDE8uF15mkil5Z2FzRm20GTJg8r3R7ldv"
        # self.headers = {"X-API-Key": self.algod_token}
        # create an algod client
        self.algod_token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        self.algod_address = "http://localhost:4001"
    def connectToNetwork(self):
        # establish connection
        return algod.AlgodClient(self.algod_token, self.algod_address)




# Player address and private key for testing
testPlayer = "32KTOUGLIPPEZTJZ4BP423LKXEURMAHK3QRHBZVIJSWPWPQED26GKHPD4Q"
playerKey = "5jCryvD1BE3R0akY48j1++dyMGWwrE97n7kmMizcjAPelTdQy0PeTM054F/NbWq5KRYA6twicOaoTKz7PgQevA=="
# usual algorand wallet for testing purpose
contractAlc = "K5UUTXMOU5CB77UHGEPBTUVKTZYZ37TJHAGYS3VIU2YMO6GGACFNJK4QD4"
toConnect = Connect()
forParam = toConnect.connectToNetwork()
# Get suggeted parameters from the network
params = forParam.suggested_params()
note = "Congratulation, you just won 100 ALGO".encode()

randomSeal = random.randint(int(49799), int(50001))
print("Random seal = {}".format(randomSeal))
winRounds = int(0)
playRound = int(0)
ACTIVE = True
max_reward = (int(100000000))
# Account 1 host
passphrase = "ugly easily drastic virus wrist same warrior sniff candy mobile embark bike relax sword unlock wedding order review flee swing such cushion record about pumpkin"
# # passphrase = "25-word-mnemonic<PLACEHOLDER>"
private_key_alc = mnemonic.to_private_key(passphrase)

defaultAddr = "NMFIFD6EJGV3X57KEEIJQDD4C7XKBLP6ZZGINO3BKASG7FYGRIU4CE62VQ"
# transfering Algo from one account to another


def payAlgo(senderAddr, rec, prvKey):
    global ACTIVE
    min_pay_1 = int(5000000)
    min_pay_2 = int(2000000)
    if ACTIVE:
        # Get account balance of supposedly contract account
        balance = forParam.account_info(
            defaultAddr)['amountwithoutpendingrewards']
        check = bool(balance >= (min_pay_1+min_pay_2))
        # transaction parameters
        txn = {
            "sender": senderAddr,
            "receiver": rec,
            "fee": params.get('minFee'),
            "flat_fee": True,
            "amt": max_reward,
            "first": params.get('lastRound'),
            "last": params.get('lastRound') + 1000,
            "note": note,
            "gen": params.get('genesisID'),
            "gh": params.get('genesishashb64')
        }
        # validate token transfer
        if((senderAddr != (defaultAddr or contractAlc)) and ((playRound == int(0)) and winRounds == int(0))):
            if(len(senderAddr) == int(58) and (check == True)):
                ACTIVE = True
                txn["amt"] = min_pay_1
                preTrxn = transaction.PaymentTxn(**txn)
                trxn = preTrxn.sign(prvKey)
                try:
                    # return("Transaction was signed with ID : ", forParam.send_transaction(trxn, headers={'content-type': 'application/x-binary'}))
                    return("Transaction was signed with ID : ", forParam.send_transaction(trxn))

                except Exception as e:
                    print(e)
            elif(playRound > int(0) and winRounds > 0):
                ACTIVE = True
                txn["amt"] = min_pay_2
                preTrxn = transaction.PaymentTxn(**txn)
                trxn = preTrxn.sign(prvKey)
                try:
                    return("Transaction was signed with ID : ", forParam.send_transaction(trxn ))
                    # return("Transaction was signed with ID : ", forParam.send_transaction(trxn, headers={'content-type': 'application/x-binary'}))
                except Exception as e:
                    print(e)
        elif(senderAddr == (defaultAddr or contractAlc)):
            preTrxn = transaction.PaymentTxn(**txn)
            trxn = preTrxn.sign(prvKey)
            try:
                return("Transaction was signed with ID : ", forParam.send_transaction(trxn))
            except Exception as e:
                print(e)
# A class for all guessgame attributes


class GuessGame():
    """Prototyping Guessgame"""

    def __init__(self):
        self.players = []
        self.alcInfo = forParam.account_info(defaultAddr)
        self.alcdetailList = [self.alcInfo]
        self.round = [7695751]
        self.threshold = self.round.index
        # self.alcdetailList[0]['assets']['9604118']['amount'])

        # 7695751
        # self.round = [
        #     int((self.alcdetailList[0]['round'] - int(1)) - self.threshold)]
     
        self.suggestedNumbers = range(
            self.round[0]-(7695751 + int(150000)), self.round[0]+randomSeal, randomSeal)
        self.active = False

    # returns a series of suggested numbers where winning number lies
    def printSuggestedNNumbers(self):
        sug_nums_len = []
        print("Winning numbers is spotted among range : \n")
        for num in self.suggestedNumbers:
            sug_nums_len.append(num)
        print(sug_nums_len)
        print(len(sug_nums_len))

    # function for guessgame
    def guessByRound(self, arg):
        global winRounds
        global playRound
        i = GuessGame()
        i.printSuggestedNNumbers()
        self.luckyNum = self.round[0]
        while ACTIVE:
           # player = input("Enter Your Algorand Address: ")
            # key = input("Enter Your Pkey: ")
            # keyList = [key]
            # account 2 player
            player = "R3P2M6PTYK4WYADAGZ7C2AWZT5YPUWGTHTFIV62YKF42T35DS243O3CRSM"
            nmumonickey = "basket mandate awkward shine evil damage buffalo sheriff arch move raccoon sand youth exist addict harsh timber evidence clutch simple canoe when raise ability vehicle"
            key = mnemonic.to_private_key(nmumonickey)
            # assert(len(player) == 58)
            self.players.append(player)
            guessLists = []
            pay = payAlgo(player, defaultAddr, key)

            time.sleep(int(5))
            if arg != self.luckyNum:
                winRounds = winRounds
                guessLists.append(arg)
                print("Oop! This roll does not match.")
                self.active = self.active
                print("Last guess was: " + str(arg))
            elif arg == self.round[0]:
                self.active = True
                playRound = playRound+int(1)
                winRounds = winRounds+int(1)
                guessLists.append(arg)
                print("Congratulations! You won!" +
                      "\n" + "100 Algo was sent to you.")
                pay = payAlgo(defaultAddr, player, private_key_alc)
                time.sleep(int(5))
                print("You are in the {} round and {} playround.".format(
                    winRounds, playRound))
            print("Do you want to make more guess? '\n'")
            replay = input("Press 'y' to continue, 'n' to quit: ")
            replay = replay.lower()
            if(replay == "n"):
                self.active = False
                print("That was really tough right?", "\n",
                      "Algorand Blockchain is awesome!.")
                print("Your total balance is {}".format(
                    forParam.account_info(player)['amountwithoutpendingrewards']))
                sys.exit(int(1))
            elif(replay == "y"):
                self.active == True
                continue


toGuess = GuessGame()
d = toGuess.round[0]
m = toGuess.guessByRound(d)
