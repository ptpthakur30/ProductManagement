/**
 *  Contains:
 *  Toolbars , SideDrawers and Backdrop
 *  The BurgerBuilder is Passed through it as prop.children
 */
import React, { useState } from 'react'
import Aux from '../Auxiliary/Auxiliary'
import { connect } from 'react-redux'
import classes from './Layout.css'

const layout = props => {
    // if (!props.isAuthenticated) {
        
    //     // To redirect the user to the auth page if not authenticated
    //     props.history.push('/auth');
    //     // to set the redirect path once the user successfully sign in in authentication page
    //     onSetAuthRedirectPath('/checkout');
    // }

    return (
        <Aux>
            <main className={classes.Content}>
                {props.children}
            </main>
        </Aux>
    )
}
const mapStateToProps = state => {
    return {
        isAuthenticated: state.auth.token != null
    }
}
export default connect(mapStateToProps)(layout);