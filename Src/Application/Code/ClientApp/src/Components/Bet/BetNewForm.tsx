import { Formik } from 'formik';
import { IProblem } from '../../Interfaces/IPage';
import { ProblemService } from '../../Services/ProblemService';
import * as bootstrap from 'bootstrap';
import { BetService } from '../../Services/BetService';

function getFormInputClass(showError: boolean, otherClasses: string): string
{
  if (showError)
  {
    return "is-invalid " + otherClasses
  }
  return otherClasses;
}

export default function BetNewForm({ projectId, problemId, setProblem }: { projectId: string, problemId: string, setProblem: (data: IProblem) => void })
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
        setTimeout(() =>
        {
          BetService.Put(projectId, problemId, JSON.stringify(values)).then(() =>
          {
            ProblemService.Get(projectId, problemId).then(
              (data) =>
              {
                // Sets the model against the page.
                setProblem(data);

                // Resets the add new problem form.
                resetForm({});

                // Close the dialog.
                let myModalEl: HTMLElement | null = document.getElementById('newModal');
                if (myModalEl != null)
                {
                  let modal: bootstrap.Modal | null = bootstrap.Modal.getInstance(myModalEl);
                  if (modal != null)
                  {
                    modal.hide();
                  }
                }
              },
              (error) =>
              {
                alert(error);
              }
            );

            // We have finished submitting the form.
            setSubmitting(false);
          });
        }, 400);
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
          <div className="modal fade" id="newModal" tabIndex={-1} aria-labelledby="newBetModalLabel" aria-hidden="true">
            <div className="modal-dialog">
              <div className="modal-content">
                <div className="modal-header">
                  <h5 className="modal-title" id="newBetModalLabel">New Bet</h5>
                  <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div className="modal-body">
                  <p>Use the form to quickly add new bets that you hope will help solve the problem. These can be fleshed out after being created.</p>
                  <div className="mb-3">
                    <label htmlFor="new-bet-name" className="form-label">Name</label>
                    <input
                      id="new-bet-name"
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
                    <label htmlFor="new-bet-description" className="form-label">Description</label>
                    <textarea
                      id="new-bet-description"
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
                    <label htmlFor="new-bet-success" className="form-label">What measures will be used to determine if the bet was a success?</label>
                    <textarea
                      id="new-bet-success"
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
                  <button type="submit" disabled={isSubmitting} className="btn btn-primary" id="problem-new-create">
                    Add Bet
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