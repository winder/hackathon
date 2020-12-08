# search_transactions_note.py
import base64
import json
# requires Python SDK version 1.3 or higher
from algosdk.v2client import indexer

# instantiate indexer client
myindexer = indexer.IndexerClient(indexer_token="", indexer_address="http://localhost:8981")

import base64

note_prefix = '{"firstName":"John"'.encode()
response = myindexer.search_transactions(
    note_prefix=note_prefix, min_round=10968688)

print("note_prefix = " +
      json.dumps(response, indent=2, sort_keys=True))

# print first note that matches
if (len(response["transactions"]) > 0):
    print("Decoded note: {}".format(base64.b64decode(
        response["transactions"][0]["note"]).decode()))
    person_dict = json.loads(base64.b64decode(
        response["transactions"][0]["note"]).decode())
    print("First Name = {}".format(person_dict['firstName']))
