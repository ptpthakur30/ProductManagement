/**
 * Defines the action creator for the burgerBuilder container
 */
import * as actionTypes from './actionTypes'
import Axios from '../../axios-order'
// Adds the ingredient
export const addProduct = (product) => {
    return {
        type: actionTypes.ADD_PRODUCT,
        products: product
    }
}

// export const removeProduct = (ingredientName) => {
//     return {
//         type: actionTypes.DELETE_PRODUCT,
//         ingredientName: ingredientName
//     }
// }
// It will call the axios and dispatch the actions based on the response of the axios
export const initProducts = () => {
    return dispatch => {
        // Axios.interceptors.request.use(
        //     (config) => {
        //       const token = window.localStorage.getItem("token");
        //       if (token) config.headers.Authorization = `Bearer ${token}`;
        //       return config;
        //     }
        //   );
        Axios.get('https://localhost:5000/api/productmanagement/')
            .then(response =>
                dispatch(setProducts(response.data))
            ).catch(error => {
                dispatch(setProductsFailed())
            })
    }
}

// For storing the value of products on store
export const setProducts = (products) => {
    return {
        type: actionTypes.SET_PRODUCTS,
        products: products
    }
}

export const setProductsFailed = () => {
    return {
        type: actionTypes.SET_PRODUCTS_FAILED,
    }
}