import json
# requires Python SDK version 1.3 or higher
from algosdk.v2client import indexer

data = {
    "indexer_token": "",
    "indexer_address": "http://localhost:8980"
}

# instantiate indexer client
myindexer = indexer.IndexerClient(**data)
# gets transactions for an account after a timestamp

data = {
    "address": "XIU7HGGAJ3QOTATPDSIIHPFVKMICXKHMOR2FJKHTVLII4FAOA3CYZQDLG4",
    "start_time": "2020-06-03T10:00:00-05:00"
}
response = myindexer.search_transactions_by_address(**data)
print("start_time: 06/03/2020 11:00:00 = " + json.dumps(response, indent=2, sort_keys=True))


