import React from 'react';
import { Link } from "react-router-dom";

/**
* Card
* Will render a card used extensively across all the menues in the application.
* @param {props} props Object properties 
*/
export function Card({ address, text }) {
    return <Link to={address}>
        <div className="card">
            <div className="card-body text-center">
                {text}
            </div>
        </div>
    </Link>
}

export function CardGrid({ data }) {
    return data.map((t, index) =>
        <div key={index} className="col-4 p-2">
            <Card address={t.address} text={t.name} />
        </div>
    )
}