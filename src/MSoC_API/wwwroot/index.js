let uri = `api`
alert(`JS works!!`)

async function RetrieveFileWithFetch() {
    event.preventDefault();
    let filePath = document.getElementById(`filePath`).value;
    let requestUri = `${uri}/files/${filePath}`;
    let response = ""
    await fetch(requestUri, {
        method: `GET`,
    }).then(x => response = x)
        .catch(y => response = y);
    alert(response)
    console.log(response.status)
    if (response.status == 200) {
        document.getElementById("downloadFile").innerHTML = ` 
        <a id="downloadFile" href="${requestUri}" download>
            <b>download file</b>
        </a>`
    }
    else {
        document.getElementById("downloadFile").innerHTML = ` 
        <h2>File not found</h2>`
    }
}

async function RetrieveFileWithAJAX() {
    event.preventDefault();
    let filePath = document.getElementById(`filePath`).value;
    let requestUri = `${uri}/files/${filePath}`;
    let response = ""
    console.log(requestUri)
    const xhttp = new XMLHttpRequest();
    xhttp.onload = function () {
        response = this
        alert(response)
        console.log(response)
        if (response.status == 200) {
            document.getElementById("downloadFile").innerHTML = ` 
        <a id="downloadFile" href="${requestUri}" download>
            <b>download file</b>
        </a>`
        }
        else {
            document.getElementById("downloadFile").innerHTML = ` 
        <h2>File not found</h2>`
        }
    }
    xhttp.open("GET", requestUri, true);
    xhttp.send();
}

async function RetrieveContentOfFileWithAJAX() {
    event.preventDefault();
    let filePath = document.getElementById(`fileContentPath`).value;
    let requestUri = `${uri}/files/${filePath}/content`;

    let response = ""
    console.log(requestUri)
    const xhttp = new XMLHttpRequest();
    xhttp.onload = function () {
        response = this
        alert(response)
        console.log(response)
        if (response.status == 200) {
            document.getElementById("fileContent").innerHTML = `<p> ${response.response}</p>`;
        }
        else {
            document.getElementById("fileContent").innerHTML = ` 
        <h2>File not found</h2>`
        }
    }
    xhttp.open("GET", requestUri, true);
    xhttp.send();
}