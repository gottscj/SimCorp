[![Build](https://github.com/gottscj/simcorp/actions/workflows/build.yml/badge.svg)](https://github.com/gottscj/danske/actions/workflows/build.yml)
# Word counter

Word counter is a single endpoint “POST /api/wordcount”, it allows a user to upload a text file, the API performs a word count operation and returns the result.
The API will return the number of occurrences for each word in the file.

Things to note:
* Words are case insensitive
* A word is defined as a set of alpha numeric characters surrounded by white-space
* the result is sorted by number of occurrences in descending order

# Usage Docker
```console
docker build --tag danske/wordcounter . 
```
```console
docker run -p 8080:8080 danske/wordcounter
```
go to:
```
http://localhost:8080/swagger/index.html
```

