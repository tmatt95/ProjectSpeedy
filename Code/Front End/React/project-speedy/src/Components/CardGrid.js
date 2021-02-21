import React from 'react';
import { Link } from "react-router-dom";

/**
* Will render a card used extensively across all the menues in the application.
* @param {props} props Object properties 
*/
export function Card({ address, text }) {
    return <Link to={address}>
        <div className="card">
            <div className="card-body text-center">
                <div><i className="bi bi-journal grid-card-icon"></i></div>
                {text}
            </div>
        </div>
    </Link>
}

/**
 * Will render a grid of cards.
 * @param {*} param0 
 */
export function CardGrid({ data }) {
    return <>
        <div className="row">
            <div className="col-4 p-2">
                <button data-bs-toggle="modal" className="btn btn-link p-0 w-100 " data-bs-target="#newModal">
                    <div className="card">
                        <div className="card-body text-center">
                            <div><i className="bi bi-journal-plus grid-card-icon"></i></div>
                            Add New
                        </div>
                    </div>
                </button>
            </div>
            {data.map((t, index) =>
                <div key={index} className="col-4 p-2">
                    <Card address={t.address} text={t.name} />
                </div>
            )}
        </div>
    </>
}