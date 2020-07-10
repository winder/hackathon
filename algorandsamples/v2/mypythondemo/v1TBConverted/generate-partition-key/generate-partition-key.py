# account_info.py
import json
# requires Python SDK version 1.3 or higher
from algosdk.v2client import algod

try:

    # create an algod client
    algod_token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
    algod_address = "http://localhost:4001"

    # algod_token = "algod-token" < PLACEHOLDER >
    # algod_address = "algod-address" < PLACEHOLDER >
    # receiver = "receiver-address" < PLACEHOLDER >

    algod_client = algod.AlgodClient(algod_token, algod_address)
    # create logic sig
    # program = b"hex-encoded-program"
    # b"\x01\x20\x01\x00\x22 is `int 0`
    # see more info here: https://developer.algorand.org/docs/features/asc1/sdks/#accessing-teal-program-from-sdks
    program = b"\x01\x20\x01\x00\x22"
    lsig = LogicSig(program)

    # string parameter
    # arg_str = "my string"
    # arg1 = arg_str.encode()
    # lsig = transaction.LogicSig(program, args=[arg1])
    # integer parameter
    # arg1 = (123).to_bytes(8, 'big')
    # lsig = transaction.LogicSig(program, args=[arg1])

    sender = lsig.address()

    # get suggested parameters
    params = algod_client.suggested_params()
    # comment out the next two (2) lines to use suggested fees
    params.flat_fee = True
    params.fee = 1000

    # build transaction

    amount = 10000
    closeremainderto = None
    receiver = "ATTR6RUEHHBHXKUHT4GUOYWNBVDV2GJ5FHUWCSFZLHD55EVKZWOWSM7ABQ"

    # create a transaction
    txn = PaymentTxn(
        sender, params, receiver, amount, closeremainderto)

    # Create the LogicSigTransaction with contract account LogicSig
    lstx = transaction.LogicSigTransaction(txn, lsig)
    # transaction.write_to_file([lstx], "simple.stxn")

    # send raw LogicSigTransaction to network
    print("This transaction is expected to fail as it is int 0 , always false")
    txid = algod_client.send_transaction(lstx)
    print("Transaction ID: " + txid)
    wait_for_confirmation(algod_client, txid)

except Exception as e:
    print(e)
