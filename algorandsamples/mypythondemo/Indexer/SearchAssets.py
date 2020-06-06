import json
# requires Python SDK version 1.3 or higher
from algosdk.v2client import indexer

data = {
    "indexer_token": "",
    "indexer_address": "http://localhost:8980"
}

# instantiate indexer client
myindexer = indexer.IndexerClient(**data)
data = {
    "asset_id": "2044572"
}
response = myindexer.search_assets(**data)
print(json.dumps(response, indent=2, sort_keys=True))
