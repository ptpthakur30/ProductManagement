/**
 * Reducer is a fucntion that takes the old state and action and returns the new state based on the action type
 */
import * as actionTypes from '../actions/actionTypes'
import {updateObject} from '../../shared/utility'

// Initial state
const initialState = {
    products: [],
    error: false
}

const addProducts = (state, action) => {
    return {
        ...state
        }
    }

const setProducts = (state, action) => {
    return updateObject(state,
        action.products)
}

const setProductsFailed = (state, action) => {
    return updateObject(state, { error: true });
}


const reducer = (state = initialState, action) => {
    switch (action.type) {
        // for adding the ingredients
        case actionTypes.ADD_PRODUCT: return addProducts(state, action);
        case actionTypes.SET_PRODUCTS: return setProducts(state, action);
        case actionTypes.SET_PRODUCTS_FAILED: return setProductsFailed(state, action);
        default: return state;
    }
}
export default reducer;