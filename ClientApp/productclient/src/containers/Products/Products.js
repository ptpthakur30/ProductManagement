/**
 * It contains the states and wraps below
 * Modal
 * The Burger 
 * The Build Controls
 */
import React, { useState, useEffect, useCallback } from 'react'
import Aux from '../../hoc/Auxiliary/Auxiliary'
import Products from '../../components/ProductsComponent/Products'
import { useSelector, useDispatch} from 'react-redux'
import Modal from '../../components/UI/Modal/Modal'
import Axios from '../../axios-order'
import Spinner from '../../components/UI/Spinner/Spinner'
import WithErrorHandler from '../../hoc/withErrorHandler/withErrorHandler'
import * as actions from '../../store/actions/index'

const burgerBuilder = props => {
    const [showOrderSummary, setshowOrderSummary] = useState(false)

    // used to dispatch the action
    const dispatch = useDispatch();
    // useCallback for caching function so avoid call back loopings
    const onInitProducts = useCallback(() => dispatch(actions.initProducts()), [dispatch]);
    const onSetAuthRedirectPath = (path) => dispatch(actions.setAuthRedirectPath(path));

    // use selector to get the store value
    const products = useSelector(state => state.product.products);
    const error = useSelector(state => state.product.error);

    useEffect(() => {
        onInitProducts()
    }, [onInitProducts]);

    let product = error ? <p>Products cannot be loaded</p> : <Spinner />;
    if (products) {
        product = (
            <Aux>
                {/* Used to see the Products */}
                <Products products={products} />
            </Aux>
        );
    }

    return (
        <Aux>
            {product}
        </Aux>
    )
}

export default (WithErrorHandler(burgerBuilder, Axios));