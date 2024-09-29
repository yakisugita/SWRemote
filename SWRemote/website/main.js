setInterval(() => {
    const searchQuery = {
        "numAD" : document.getElementById("slider").value,
    }
    
    const urlSearchParam = new URLSearchParams(searchQuery).toString()
    
    const request = new XMLHttpRequest()
    request.open("GET", `/api/test?${urlSearchParam}`, false)
    
    request.send()
    
    if (request.status == 0) console.log("エラー")
    if (request.status != 200) console.log("NOT200")
    console.log(request.responseText)
    //const searchResult = JSON.parse(request.responseText);
    //console.log(searchResult)
}, 500);