import { IBet } from "../../Interfaces/IPage";
import { Formik } from 'formik';

function getFormInputClass(showError: boolean, otherClasses: string): string
{
    if (showError)
    {
        return "is-invalid " + otherClasses
    }
    return otherClasses;
}

function GetStarted(bet: IBet)
{
    return <>
        <h2>Name</h2>
        <div className="mb-3">{bet.name}</div>

        <h2>Description</h2>
        <p>{bet.description}</p>

        <h2>Measures of Success</h2>
        <p>{bet.successCriteria}</p>

        <h2>Time Given To Bet</h2>
    </>;
}

function GetForm(bet: IBet)
{
    return <>
        <Formik
            initialValues={{ name: bet.name, description: bet.description, successCriteria: bet.successCriteria, timeTotal: bet.timeTotal } as IBet}
            validate={values =>
            {
                const errors: { name: string, description: string, successCriteria: string, timeTotal: string } = {
                    name: "",
                    description: "",
                    successCriteria: "",
                    timeTotal: ""
                };

                // Are there any errors with the form?
                let hasError: boolean = false;

                // New problem name
                if (!values.name)
                {
                    errors.name = 'Required';
                    hasError = true;
                }

                if (Number.isNaN(values.timeTotal) === true)
                {
                    errors.timeTotal = 'Please eter a valid number';
                    hasError = true;
                }

                // If there are errors then display them.
                if (hasError === true)
                {
                    return errors;
                }
                else
                {
                    return {};
                }
            }}
            onSubmit={(values, { setSubmitting, setErrors, setStatus, resetForm }) =>
            {
                alert("do save");
            }}
        >
            {({
                values,
                errors,
                touched,
                handleChange,
                handleBlur,
                handleSubmit,
                handleReset,
                isSubmitting,
                /* and other goodies */
            }) => (
                <form onReset={handleReset} onSubmit={handleSubmit}>
                    <h1>Bet</h1>
                    <p>Status: {bet.status}</p>
                    <h2>
                        <label htmlFor="name" className="form-label">Name</label>
                    </h2>
                    <div className="mb-3">
                        <input
                            id="name"
                            type="text"
                            name="name"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values.name}
                            className={getFormInputClass(errors !== undefined && errors.name !== undefined && errors.name.length > 0, "form-control")}
                        />
                        <div id="validationNameFeedback" className="invalid-feedback">
                            {errors.name && touched.name && errors.name}
                        </div>
                    </div>

                    <h2><label htmlFor="description" className="form-label">Description</label></h2>
                    <div className="mb-3">
                        <textarea
                            id="description"
                            name="description"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values.description}
                            className={getFormInputClass(errors !== undefined && errors.description !== undefined && errors.description.length > 0, "form-control")}
                        ></textarea>
                        <div id="validationDescriptionFeedback" className="invalid-feedback">
                            {errors.description && touched.description && errors.description}
                        </div>
                    </div>

                    <h2>
                        <label htmlFor="success" className="form-label">Measures of Success</label>
                    </h2>
                    <div className="mb-3">
                        <textarea
                            id="success"
                            name="successCriteria"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values.successCriteria}
                            className={getFormInputClass(errors !== undefined && errors.successCriteria !== undefined && errors.successCriteria.length > 0, "form-control")}
                        ></textarea>
                        <div id="validationSuccessFeedback" className="invalid-feedback">
                            {errors.successCriteria && touched.successCriteria && errors.successCriteria}
                        </div>
                    </div>

                    <h2>Time Given To Bet (in days)</h2>
                    <div className="mb-3">
                        <input
                            id="timeTotal"
                            type="number"
                            name="timeTotal"
                            min="0"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values.timeTotal}
                            className={getFormInputClass(errors !== undefined && errors.timeTotal !== undefined && errors.timeTotal.length > 0, "form-control")}
                        />
                        <div id="validationNameFeedback" className="invalid-feedback">
                            {errors.timeTotal && touched.timeTotal && errors.timeTotal}
                        </div>
                    </div>
                    <div className="row mb-3">
                        <div className="col">
                            <button className="btn btn-danger">Cancel</button>
                        </div>
                        <div className="col text-end">
                            <button type="submit" disabled={isSubmitting} className="btn btn-primary" id="bet-update">Save</button>
                        </div>
                    </div>
                </form>
            )}
        </Formik>
    </>
}

export default function BetForm({ bet }: { bet: IBet })
{
    switch (bet.status)
    {
        case "Created": {
            return GetForm(bet);
        }
        case "In Progress": {
            return GetStarted(bet);
        }
        case "Finished": {
            return GetStarted(bet);
        }
    }
    return <></>;
}