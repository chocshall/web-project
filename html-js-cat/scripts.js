//// stores the entire div element and now it can be manipulated
//const app = document.getElementById('root')
//const logo = document.createElement('img')
//logo.src = 'images/logo.png';
//const container = document.createElement('div')
//container.setAttribute('class', 'container')

//app.appendChild(logo)
//app.appendChild(container)

// Create a request variable and assign a new XMLHttpRequest object to it.


var request = new XMLHttpRequest()

// get the value from page with catCount name
const count = document.getElementById('catCount').value

// Open a new connection, using the GET request on the URL endpoint
request.open('GET', `https://api.thecatapi.com/v1/images/search?limit=${count}&api_key=live_HKPwwklMQG87g8QSSMUEabTdkJLxTyxVUXfkDqV736o5J68Spz3sqx2tYiH2xsFC`, true)

request.onload = function () {
    
    // Begin accessing JSON data here
    var data = JSON.parse(this.response)

    if (request.status >= 200 && request.status < 400) {
        data.forEach((cat) => {
            // Log each movie's title
            console.log(cat.url)
            const img = document.createElement('img')
            img.src = cat.url
            catHolder.appendChild(img);
        })
    }

    else
    {
        console.log("error")
    }
   
}

// Send request
request.send()