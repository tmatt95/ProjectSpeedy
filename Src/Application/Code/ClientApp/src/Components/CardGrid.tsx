import { MouseEvent } from 'react';
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

    /**
     * css class for the icon
     */
     iconClasses: string;
}

/**
* Will render a card used extensively across all the menues in the application.
* @param CardItem props Object properties 
*/
function Card({ address, name, iconClasses }: CardItem)
{
    return <Link to={address}>
        <div className="card">
            <div className="card-body text-center">
                <div><i className={iconClasses}></i></div>
                {name}
            </div>
        </div>
    </Link>
}

/**
 * Will render a grid of cards.
 * @param {*} param0 
 */
export function CardGrid({ data, AddNewClick }: { data: CardItem[], AddNewClick: (e: MouseEvent) => void })
{
    return <>
        <div className="row">
            <div className="col-md-6 col-lg-4 pt-2">
                <button className="btn btn-link p-0 w-100" onClick={AddNewClick} id="add-new">
                    <div className="card">
                        <div className="card-body text-center">
                            <div><i className="bi bi-plus-square-dotted grid-card-icon"></i></div>
                            Add New
                        </div>
                    </div>
                </button>
            </div>
            {data.map((t, index: number) =>
                <div key={index} className="col-md-6 col-lg-4 pt-2">
                    <Card address={t.address} name={t.name} iconClasses={t.iconClasses} />
                </div>
            )}
        </div>
    </>
}