import { Formik, FormikState } from 'formik';

function getFormInputClass(showError: boolean, otherClasses: string): string
{
  if (showError)
  {
    return "is-invalid " + otherClasses
  }
  return otherClasses;
}

export default function ProblemBetNewForm({ title, description, buttonText, saveAction }: {
  title: string,
  description: string,
  buttonText: string,
  saveAction: (
    values: { name: string, description: string, successCriteria: string },
    setSubmitting: (isSubmitting: boolean) => void,
    resetForm: (nextState?: Partial<FormikState<{
        name: string;
        description: string;
        successCriteria: string;
    }>> | undefined) => void
  ) => void
})
{
  return (<>
    <Formik
      initialValues={{ name: '', description: '', successCriteria: '' }}
      validate={values =>
      {
        const errors: { name: string, description: string, successCriteria: string } = {
          name: "",
          description: "",
          successCriteria: ""
        };

        // Are there any errors with the form?
        let hasError: boolean = false;

        // New problem name
        if (!values.name)
        {
          errors.name = 'Required';
          hasError = true;
        }

        // New problem description.
        if (!values.description)
        {
          errors.description = 'Required';
          hasError = true;
        }

        // New problem success criteria.
        if (!values.successCriteria)
        {
          errors.successCriteria = 'Required';
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
        // Carries out the save action
        saveAction(values, setSubmitting, resetForm);
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
        <form onReset={handleReset} onSubmit={handleSubmit} id="new-form">
          <div className="modal fade" id="newModal" tabIndex={-1} aria-labelledby="newProblemBetModalLabel" aria-hidden="true">
            <div className="modal-dialog">
              <div className="modal-content">
                <div className="modal-header">
                  <h5 className="modal-title" id="newProblemBetModalLabel">{title}</h5>
                  <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div className="modal-body">
                  <p>{description}</p>
                  <div className="mb-3">
                    <label htmlFor="new-problem-bet-name" className="form-label">Name</label>
                    <input
                      id="new-problem-bet-name"
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

                  <div className="mb-3">
                    <label htmlFor="new-problem-bet-description" className="form-label">Description</label>
                    <textarea
                      id="new-problem-bet-description"
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

                  <div className="mb-3">
                    <label htmlFor="new-problem-bet-success" className="form-label">What measures will be used to determine when the problem has been fixed?</label>
                    <textarea
                      id="new-problem-bet-success"
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
                </div>
                <div className="modal-footer">
                  <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                  <button type="submit" disabled={isSubmitting} className="btn btn-primary" id="problem-bet-new-create">
                    {buttonText}
                  </button>
                </div>
              </div>
            </div>
          </div>
        </form>
      )}
    </Formik>
  </>);
}