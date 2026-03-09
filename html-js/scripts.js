// stores the entire div element and now it can be manipulated
const app = document.getElementById('root')
const logo = document.createElement('img')
logo.src = 'images/logo.png';

const container = document.createElement('div')
container.setAttribute('class', 'container')

app.appendChild(logo)
app.appendChild(container)

// can retrieved data from from url wihout having to do a page refresh
var request = new XMLHttpRequest()
request.open('GET', 'https://ghibliapi.dev/films', true)
// when it loads data succesfully do something
request.onload = function () {
    // accesing json data here
    // the same as request.response
    var data = JSON.parse(this.response)
    // checks if the url is fine else show error code
    if (request.status >= 200 && request.status < 400)
    {
        // movies is just a random parameter name that was chosen
        data.forEach((movie) => {
           
            //creates a element by name div with card class
            const card = document.createElement('div')
            card.setAttribute('class', 'card')

            // creates a placeholder that will eventually display the data 
            const h1 = document.createElement('h1')
            h1.textContent = movie.title

            const p = document.createElement('p')

            movie.description = movie.description.substring(0, 300) // limit to 300 chars

            p.textContent = `${movie.description}...` // end with an three dots
            // need to use ` the backtick so it would work with variables

            // this is how it add elements to the page it append to the container which was created earlier
            container.appendChild(card)

            // each card will hold movie title and movie description
            
            card.appendChild(h1)
            card.appendChild(p)
           
        })
    }
    else
    {
        console.log('error')
    }

}

request.send()