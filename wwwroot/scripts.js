const app = document.getElementById('root')

const container = document.createElement('div')
container.setAttribute('class', 'container')

app.appendChild(container)

var request = new XMLHttpRequest()

//const count = document.getElementById('catCount').value

// Open a new connection, using the GET request on the URL endpoint
// true paramer says that its async request means the code does not pause untill the request is complete
//request.open('GET', 'https://233d90c1-3689-444f-88e9-1edaa85a5c75.mock.pstmn.io/api/School/Students', true)
request.open('GET', 'https://localhost:7268/api/School/Students', true)

request.onload = function () {

    // Begin accessing JSON data here
    var data = JSON.parse(this.response)

    

    const uiHolder = document.createElement('div')
    uiHolder.setAttribute('class', 'uiHolder')

    // placeholder for data showing
    const h1 = document.createElement('h1')
    h1.textContent = 'Student box'

    const p = document.createElement('p')

    p.textContent = ''

    if (request.status >= 200 && request.status < 400) {
        data.forEach((student) => {
            
            console.log(student.firstName)

            

            p.textContent += `${student.firstName} ${student.lastName} \n`
            

            container.appendChild(uiHolder)

            // each card will hold movie title and movie description

            uiHolder.appendChild(h1)
            uiHolder.appendChild(p)
        })
    }

    else {
        console.log("error")
    }

}

// Send request
request.send()