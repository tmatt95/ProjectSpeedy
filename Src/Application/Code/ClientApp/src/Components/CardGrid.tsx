import { MouseEvent, useState } from 'react';
import * as bootstrap from 'bootstrap';
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
function Card({ address, name }: CardItem)
{
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
export function CardGrid({ data }: { data: CardItem[] })
{
    const [dialogOpened, setDialogOpened] = useState(false);

    /**
     * Resets the form validators etc.
     */
    function ResetForm()
    {
        let form: HTMLFormElement | null = document.getElementById("new-form") as HTMLFormElement;
        if (form !== null)
        {
            form.reset();
        }
    }

    /**
     * Loads the modal onto the screen.
     */
    function DisplayModal(e: MouseEvent)
    {
        e.preventDefault();
        let myModalEl: HTMLElement | null = document.getElementById('newModal');
        if (myModalEl !== null)
        {
            if (dialogOpened == false)
            {
                // Resets when dialog open
                myModalEl.addEventListener('show.bs.modal', function (event)
                {
                    ResetForm();
                });
                setDialogOpened(true);
            }

            // Opens the modal.
            let modal = new bootstrap.Modal(myModalEl, { keyboard: false });
            if (modal != null)
            {
                modal.show();
            }
        }
    }

    return <>
        <div className="row">
            <div className="col-md-6 col-lg-4 pt-2">
                <button className="btn btn-link p-0 w-100" onClick={DisplayModal} id="add-new">
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