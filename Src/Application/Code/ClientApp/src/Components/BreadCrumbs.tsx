import React from 'react';
import { Link } from "react-router-dom";

/**
 * Represents an individual breadcrumb item.
 */
export interface BreadCrumbItem
{
    /**
     * Address the user is taken to when they click a breadcrumb.
     */
    address: string;

    /**
     * Text to display in the breadcrumb.
     */
    text: string;

    /**
     * If the breadcrumb is the last element in the list.
     */
    isLast: boolean;
}

/**
* Will render a breadcrumb.
* @param {props} props Object properties 
*/
export function BreadCrumb({ address, text, isLast }: BreadCrumbItem) {
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
 * @param {breadCrumbs: BreadCrumbItem[]} props sent into the breadcrumbs. 
 */
export function BreadCrumbs({ breadCrumbs }: {breadCrumbs: BreadCrumbItem[]}) {
    return <>
        <nav aria-label="breadcrumb">
            <ol className="breadcrumb">
            {breadCrumbs.map((breadCrumb: BreadCrumbItem, index: number) =>
                <BreadCrumb key={index} {...breadCrumb} isLast={(index+1 === breadCrumbs.length)} />
            )}
            </ol>
        </nav>
    </>
}