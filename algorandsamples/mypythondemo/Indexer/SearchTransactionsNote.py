import json
# requires Python SDK version 1.3 or higher
from algosdk.v2client import indexer

data = {
    "indexer_token": "",
    "indexer_address": "http://localhost:8980"
}
# instantiate indexer client
myindexer = indexer.IndexerClient(**data)
import base64
encodednote = base64.b64encode('showing prefix'.encode())
data = {
    "note_prefix": base64.b64decode(encodednote)
}
response = myindexer.search_transactions(**data)
print("note_prefix = " +
      json.dumps(response, indent=2, sort_keys=True))
