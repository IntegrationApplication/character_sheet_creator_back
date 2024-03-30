#!/usr/bin/env bash

curl -X 'POST' \
  'http://0.0.0.0:8080/Character/CreateCharacter?idPlayer=1&idGame=1' \
  -H 'accept: text/plain' \
  -d ''
