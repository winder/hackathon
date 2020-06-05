import json
# requires Python SDK version 1.3 or higher
from algosdk.v2client import indexer

data = {
    "indexer_token": "",
    "indexer_address": "http://localhost:8980"
}

# instantiate indexer client
myindexer = indexer.IndexerClient(**data)
# gets tranactins for an account after a timestamp


# min/max rounds
data = {
    "address": "SWOUICD7Y5PQBWWEYC4XZAQZI7FJRZLD5O3CP4GU2Y7FP3QFKA7RHN2WJU",
    "asset_id": "2044572",
    "min_amount": 50
    }
response = myindexer.search_transactions_by_address(**data)
print(json.dumps(response, indent=2, sort_keys=True))
