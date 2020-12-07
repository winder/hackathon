import json
import base64
from algosdk.v2client import algod
from algosdk import mnemonic
from algosdk.future.transaction import PaymentTxn

# utility for waiting on a transaction confirmation
def wait_for_confirmation(client, transaction_id, timeout):
    """
    Wait until the transaction is confirmed or rejected, or until 'timeout'
    number of rounds have passed.
    Args:
        transaction_id (str): the transaction to wait for
        timeout (int): maximum number of rounds to wait    
    Returns:
        dict: pending transaction information, or throws an error if the transaction
            is not confirmed or rejected in the next timeout rounds
    """
    start_round = client.status()["next-version-round"]
    current_round = start_round

    while current_round <= start_round + timeout:
        client.status_after_block(current_round)      
        try:
            pending_txn = client.pending_transaction_info(transaction_id)
            # raise Exception('pool error: {}'.format(pending_txn["pool-error"]))
        except Exception:
            return 
        if pending_txn.get("confirmed-round", 0) > 0:
            return pending_txn
        elif pending_txn["pool-error"]:  
            raise Exception(
                'pool error: {}'.format(pending_txn["pool-error"]))
        current_round += 1
    raise Exception(
        'pending tx not found in timeout rounds, timeout value = : {}'.format(timeout))

def send_note():
    # Use sandbox or your address and token
    algod_address = "<your-algod-node-and-port>"
    algod_token = "<your-api-token>"
    algod_client = algod.AlgodClient(algod_token, algod_address)

    algod_address = "http://localhost:4001"
    algod_token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
    algod_client = algod.AlgodClient(algod_token, algod_address)

    #passphrase = "<your-25-word-mnemonic>"
    passphrase = "price clap dilemma swim genius fame lucky crack torch hunt maid palace ladder unlock symptom rubber scale load acoustic drop oval cabbage review abstract embark"
    private_key = mnemonic.to_private_key(passphrase)
    my_address = mnemonic.to_public_key(passphrase)
    print("My address: {}".format(my_address))
    params = algod_client.suggested_params()
    # comment out the next two (2) lines to use suggested fees
    params.flat_fee = True
    params.fee = 1000
    note = '{"firstName":"John", "lastName":"Doe"}'.encode()
    receiver = "GD64YIY3TWGDMCNPP553DZPPR6LDUSFQOIJVFDPPXWEG3FVOJCCDBBHU5A"

    unsigned_txn = PaymentTxn(my_address, params, receiver, 1000000, None, note)

    # sign transaction
    signed_txn = unsigned_txn.sign(private_key)
    # send transaction
    txid = algod_client.send_transaction(signed_txn)
    print("Send transaction with txID: {}".format(txid))

    # wait for confirmation
    try:
        confirmed_txn = wait_for_confirmation(algod_client, txid, 4)  
    except Exception as err:
        print(err)
        return
    
    print("txID: {}".format(txid), " confirmed in round: {}".format(
        confirmed_txn.get("confirmed-round", 0)))       
    print("Transaction information: {}".format(
        json.dumps(confirmed_txn, indent=2)))
    print("Decoded note: {}".format(base64.b64decode(
        confirmed_txn["txn"]["txn"]["note"]).decode()))        
    person_dict = json.loads(base64.b64decode(
        confirmed_txn["txn"]["txn"]["note"]).decode())
    print("First Name = {}".format(person_dict['firstName']))

send_note()
