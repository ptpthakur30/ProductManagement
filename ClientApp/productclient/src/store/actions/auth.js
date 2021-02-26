// Action creator for Authentication
import * as actionTypes from './actionTypes'
import axios from '../../axios-order'

export const authStart = () => {
    return {
        type: actionTypes.AUTHENTICATION_START
    }
}

export const authSuccess = (token, userId) => {
    return {
        type: actionTypes.AUTHENTICATION_SUCCESS,
        token: token,
        userId: userId
    }
}

// To set the time of the logout
export const checkAuthTimeout = (expirationTime) => {
    return {
        type : actionTypes.AUTHENTICATION_CHECK_TIMEOUT,
        expirationTime : expirationTime
    }
}

export const auth = (email, password, isSignup) => {
    return dispatch => {
        dispatch(authStart())
        const authData = {
            email: email,
            password: password,
            returnSecureToken: true
        }
        // We just differentiate url based on the isSignUp value 
        let url = 'https://localhost:5000/api/user/login'
       
        axios.post(url, authData)
            .then(response => {
                
                dispatch(authSuccess(response.data.idToken, response.data.localId))

                // Setting the local storage to store the auth data even if we close the application
                const expirationDate = new Date(new Date().getTime() + response.data.expiresIn * 1000)
                localStorage.setItem('token', response.data.token);
                localStorage.setItem('expirationDate', expirationDate);
                localStorage.setItem('username', response.data.username);

                // To set the logout time , i.e null the token
                dispatch(checkAuthTimeout(response.data.expiresIn))

            })
            .catch(err => {
                dispatch(authFail(err.response.data.error))
            })
    };
}

export const authFail = (error) => {
    return {
        type: actionTypes.AUTHENTICATION_FAIL,
        error: error
    }
}
export const authLogout = () => {
    return {
        type: actionTypes.AUTHENTICATION_INITIATE_LOGOUT
    }
}

// If the logout succeed
export const logoutsucceed = ()=>{
    return {
        type: actionTypes.AUTHENTICATION_LOGOUT
    }
}

export const setAuthRedirectPath = (path) => {
    return {
        type: actionTypes.AUTHENTICATION_REDIRECTPATH,
        path: path
    }
}

// To check if the local storage is set and to set the auth token,userid and expiry based on local storage 
export const checkAuthState = () => {
    return dispatch => {
        const token = localStorage.getItem('token');
        if (!token) {
            dispatch(authLogout());
        }
        else {
            // To get the expiration date
            const expirationDate = new Date(localStorage.getItem('expirationDate'))
            if (expirationDate > new Date()) {
                const userId = localStorage.getItem('username');
                // To set the token and userId in the central store
                dispatch(authSuccess(token, userId));
                // To get the milli seconds
                dispatch(checkAuthTimeout((expirationDate.getTime()-new Date().getTime())/1000))
            }
            else {
                dispatch(authLogout());
            }
        }
    }
}