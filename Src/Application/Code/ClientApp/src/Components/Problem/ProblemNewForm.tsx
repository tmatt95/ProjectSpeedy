import { Formik } from 'formik';
import * as bootstrap from 'bootstrap';
import { ProjectService } from '../../Services/ProjectService';
import { CardItem } from '../CardGrid';

function getFormInputClass(showError: boolean, otherClasses: string): string
{
  if (showError)
  {
    return "is-invalid " + otherClasses
  }
  return otherClasses;
}

export default function ProblemNewForm({ setProblems }: { setProblems: (problems: CardItem[]) => void })
{
  return (<>
    <Formik
      initialValues={{ name: '' }}
      validate={values =>
      {
        const errors: { name: string } = {
          name: ""
        };
        let hasError: boolean = false;
        if (!values.name)
        {
          errors.name = 'Required';
          hasError = true;
        }
        if (hasError === true)
        {
          return errors;
        }
        else
        {
          return {};
        }
      }}
      onSubmit={(values,  {setSubmitting, setErrors, setStatus, resetForm}) =>
      {
        setTimeout(() =>
        {
          setSubmitting(false);
          ProjectService.Put(JSON.stringify(values)).then(() =>
          {
            ProjectService.GetAll().then(
              (data) =>
              {
                setProblems(data);
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
        isSubmitting,
        /* and other goodies */
      }) => (
        <form onSubmit={handleSubmit} id="new-project-form">
          <div className="modal fade" id="newModal" tabIndex={-1} aria-labelledby="newProjectModalLabel" aria-hidden="true">
            <div className="modal-dialog">
              <div className="modal-content">
                <div className="modal-header">
                  <h5 className="modal-title" id="newProjectModalLabel">New Project</h5>
                  <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div className="modal-body">
                  <p>Use the form to quickly add projects. These can be fleshed out after being created.</p>
                  <input
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
                <div className="modal-footer">
                  <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                  <button type="submit" disabled={isSubmitting} className="btn btn-primary" id="project-new-create">
                    Add Project
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