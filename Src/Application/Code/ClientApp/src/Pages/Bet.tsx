import { Dispatch, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { IBet, IPage } from "../Interfaces/IPage";
import { BetService } from "../Services/BetService";
import { Formik, FormikErrors, FormikTouched } from 'formik';

export function Bet(pageProps: IPage)
{

    /**
         * GET parameters.
         */
    let { problemId, projectId, betId }: { problemId: string, projectId: string, betId: string } = useParams();

    /**
     * Page model definition.
     */
    var defaultBet: IBet = { name: "", status: "", description: "", successCriteria: "", isLoaded: false, timeCurrent: 0, timeTotal: 0 };
    const [bet, setBet]: [IBet, Dispatch<IBet>] = useState(defaultBet);

    /**
     * Used to run code only once on page load.
     */
    const [runOnce, setRunOnce] = useState(false);
    useEffect(() =>
    {
        if (runOnce === false)
        {
            setRunOnce(true);

            // Loads the projects onto the page
            BetService.Get(projectId, problemId, betId).then(
                (data) =>
                {
                    // Sets the model against the page.
                    setBet(data);
                    data.isLoaded = true;

                    // Sets the project name.
                    document.title = `Bet ${bet.name}`;

                    // Set the breadcrumbs.
                    pageProps.setBreadCrumbs([
                        { address: "/", text: "Projects", isLast: false },
                        { address: `/project/${projectId}`, text: "Project Name", isLast: false },
                        { address: `/project/${projectId}/problem/${problemId}`, text: "ProblemName", isLast: false },
                        { address: `/project/${projectId}/problem/${problemId}/bet/${betId}`, text: "BetName", isLast: true }
                    ]);
                },
                (error) =>
                {
                    alert(error);
                }
            );
        }
    }, [runOnce, pageProps, projectId, problemId, bet.name, betId]);

    function getFormInputClass(showError: boolean, otherClasses: string): string
    {
        if (showError)
        {
            return "is-invalid " + otherClasses
        }
        return otherClasses;
    }

    function getFormSection(
        handleChange: {
            (e: React.ChangeEvent<any>): void;
            <T = string | React.ChangeEvent<any>>(field: T): T extends React.ChangeEvent<any> ? void : (e: string | React.ChangeEvent<any>) => void;
        },
        handleBlur: {
            (e: React.FocusEvent<any>): void;
            <T = any>(fieldOrEvent: T): T extends string ? (e: any) => void : void;
        },
        errors: FormikErrors<{
            name: string;
            description: string;
            successCriteria: string;
            timeTotal: string;
        }>,
        touched: FormikTouched<{
            name: string;
            description: string;
            successCriteria: string;
            timeTotal: string;
        }>,
        values: IBet
    )
    {
        if (bet.status === "Created")
        {
            return <>
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
            </>
        }
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

    if (bet.isLoaded !== true)
    {
        return <></>;
    }

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
                    {getFormSection(handleChange, handleBlur, errors, touched, values)}
                </form>
            )}
        </Formik>

        <div className="mb-3">
            <h2>Start Bet</h2>
            <p>When your ready to begin work on the bet click start. The time will start counting down and the bet will become active. Good luck!</p>
            <button className="btn btn-primary">Start bet</button>
        </div>

        <nav className="mt-3">
            <div className="nav nav-tabs" id="nav-tab" role="tablist">
                <button className="nav-link active" id="nav-comments-tab" data-bs-toggle="tab" data-bs-target="#nav-comments" type="button" role="tab" aria-controls="nav-comments" aria-selected="true">Comments</button>
                <button className="nav-link" id="nav-feedback-tab" data-bs-toggle="tab" data-bs-target="#nav-feedback" type="button" role="tab" aria-controls="nav-feedback" aria-selected="false">Feedback</button>
                <button className="nav-link" id="nav-outcomes-tab" data-bs-toggle="tab" data-bs-target="#nav-outcomes" type="button" role="tab" aria-controls="nav-outcomes" aria-selected="false">Outcomes</button>
            </div>
        </nav>
        <div className="tab-content" id="nav-tabContent">
            <div className="tab-pane fade show active" id="nav-comments" role="tabpanel" aria-labelledby="nav-comments-tab">...</div>
            <div className="tab-pane fade" id="nav-feedback" role="tabpanel" aria-labelledby="nav-feedback-tab">...</div>
            <div className="tab-pane fade" id="nav-outcomes" role="tabpanel" aria-labelledby="nav-outcomes-tab">...</div>
        </div>
    </>;
}