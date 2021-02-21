import React from 'react';
import { Link } from "react-router-dom";

/**
* Will render a breadcrumb.
* @param {props} props Object properties 
*/
export function BreadCrumb({ address, text, isLast }) {
    if(isLast === false){
        return <>
            <li className="breadcrumb-item"><Link to={address}>{text}</Link></li>
        </>
    } else{
        return <>
            <li className="breadcrumb-item active" aria-current="page">{text}</li>
        </>
    }
}

/**
 * Will render the bread crumbs.
 * @param {*} param0 
 */
export function BreadCrumbs({ breadCrumbs }) {
    return <>
        <nav aria-label="breadcrumb">
            <ol className="breadcrumb">
            {breadCrumbs.map((breadCrumb, index) =>
                <BreadCrumb key={index} {...breadCrumb} isLast={(index+1 === breadCrumbs.length)} />
            )}
            </ol>
        </nav>
    </>
}