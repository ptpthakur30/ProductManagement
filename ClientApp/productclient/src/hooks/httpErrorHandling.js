/**
 * It is custom hook used to get the 
 */
import { useState, useEffect } from 'react'

export default httpclient => {
    const [error, seterror] = useState(null);

    const closeModalHandler = () => {
        seterror(null)
    }

    
    const reqInterceptor = httpclient.interceptors.request.use(req => {
        seterror(null);
        return req;
    })
    const resInterceptor = httpclient.interceptors.response.use(resp => resp, err => {
        seterror(err)
    })
    
    useEffect(() => {
        
        return () => {
            httpclient.interceptors.request.eject(reqInterceptor);
            httpclient.interceptors.response.eject(resInterceptor);
        }
    }, [reqInterceptor, resInterceptor])

    // returns the error and clear error function
    return [error, closeModalHandler];
}