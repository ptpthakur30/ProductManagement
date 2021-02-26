import Axios from 'axios'

const instance = Axios.create({
    baseURL : 'http://localhost:5000/api'
})

export default instance
