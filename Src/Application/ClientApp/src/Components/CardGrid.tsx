import React from 'react';
import { Link } from "react-router-dom";

/**
 * Represents an individual breadcrumb item.
 */
 export interface CardItem
 {
     /**
      * Address the user is taken to when they click a breadcrumb.
      */
     address: string;
 
     /**
      * Text to display in the breadcrumb.
      */
     name: string;
 }

/**
* Will render a card used extensively across all the menues in the application.
* @param CardItem props Object properties 
*/
export function Card({ address, name }: CardItem) {
    return <Link to={address}>
        <div className="card">
            <div className="card-body text-center">
                <div><i className="bi bi-journal grid-card-icon"></i></div>
                {name}
            </div>
        </div>
    </Link>
}

/**
 * Will render a grid of cards.
 * @param {*} param0 
 */
export function CardGrid({ data }: {data: CardItem[]}) {
    return <>
        <div className="row">
            <div className="col-md-6 col-lg-4 pt-2">
                <button data-bs-toggle="modal" className="btn btn-link p-0 w-100 " data-bs-target="#newModal">
                    <div className="card">
                        <div className="card-body text-center">
                            <div><i className="bi bi-journal-plus grid-card-icon"></i></div>
                            Add New
                        </div>
                    </div>
                </button>
            </div>
            {data.map((t, index: number) =>
                <div key={index} className="col-md-6 col-lg-4 pt-2">
                    <Card address={t.address} name={t.name} />
                </div>
            )}
        </div>
    </>
}