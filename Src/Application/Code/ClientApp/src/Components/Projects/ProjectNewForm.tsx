import { Formik } from 'formik';
import { ProjectService } from '../../Services/ProjectService';
import { CardItem } from '../CardGrid';
import { PageFunctions } from '../../Pages/PageFunctions';

function getFormInputClass(showError: boolean, otherClasses: string): string
{
  if (showError)
  {
    return "is-invalid " + otherClasses
  }
  return otherClasses;
}

export default function ProjectNewForm({ setProjects }: { setProjects: (data: CardItem[]) => void })
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
          ProjectService.Put(JSON.stringify(values)).then(async (saveResponse) =>
          {
            if (saveResponse.status !== 202)
            {
              // Display error
              const json:{message:string} = await saveResponse.json();
              setErrors({ name: json.message });

              // Refresh data behind dialog.
              ProjectService.GetAll().then(
              (data) =>
              {
                // Display projects on page.
                setProjects(data);
              });
            }
            else
            {
              ProjectService.GetAll().then(
                (data) =>
                {
                  // Display projects on page.
                  setProjects(data);
  
                  // Reset form.
                  resetForm({});
  
                  // Close the dialog.
                  PageFunctions.CloseDialog('newModal');
                },
                (error) =>
                {
                  alert(error);
                }
              );
            }
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
          <div className="modal fade" id="newModal" tabIndex={-1} aria-labelledby="newProjectModalLabel" aria-hidden="true">
            <div className="modal-dialog">
              <div className="modal-content">
                <div className="modal-header">
                  <h2 className="modal-title" id="newProjectModalLabel">New Project</h2>
                  <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div className="modal-body">
                  <p>Use the form to quickly add projects. These can be fleshed out after being created.</p>
                  <label htmlFor="new-project-name" className="form-label">Name</label>
                  <input
                    id="new-project-name"
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