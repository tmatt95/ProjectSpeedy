import React, { useState } from 'react';
import { Projects } from './Pages/Projects';
import { Project } from './Pages/Project';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  useRouteMatch,
} from "react-router-dom";

/**
 * Display a global message across the website.
 * @param {*} Message object containing the message and display options.
 */
function GlobalMessage({ message }) {
  if (message !== "") {
    let classes = `alert ${message.class}`;
    return <div className={classes} role="alert">
      {message.message}
    </div>;
  }
  else {
    return <div></div>
  }
}

export default function App() {
  const [count, setCount] = useState(0);
  const [message, setMessage] = useState("");
  return (
    <>
      <Router>
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
          <div className="container">
            <span className="navbar-brand mb-0 h1">Project Speedy</span>
            <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
              <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarNav">
              <ul className="navbar-nav">
                <li className="nav-item">
                  <Link className="nav-link" to="/">Projects</Link>
                </li>
              </ul>
            </div>
          </div>
        </nav>

        <div className="container">
          <div className="row">
            <div className="col">
              <GlobalMessage message={message} />
              <p>You clicked {count} times</p>
              <button onClick={() => setCount(count + 1)}>
                Click me
              </button>
            </div>
          </div>
        </div>

        <div className="container">
        <Switch>
          <Route path="/project">
            <ProjectSection globalMessage={(alertMessage) => setMessage(alertMessage)} />
          </Route>
          <Route path="/">
            <Projects globalMessage={(alertMessage) => setMessage(alertMessage)} />
          </Route>
        </Switch>
        </div>
      </Router>
    </>
  );
}

/**
 * Contains links to the project section of the application.
 * @param {*} props Properties sent into the section.
 */
function ProjectSection(props) {
  let match = useRouteMatch();
  return (
    <div>
      <Switch>
        <Route path={`${match.path}/:projectId/bet/:betId`}>
          <h3>Bet</h3>
        </Route>
        <Route path={`${match.path}/:projectId/bet/`}>
          <h3>Bet id not supplied</h3>
        </Route>
        <Route path={`${match.path}/:projectId`}>
          <Project globalMessage={props.globalMessage} />
        </Route>
        <Route path={match.path}>
          <h3>Project Id not supplied</h3>
        </Route>
      </Switch>
    </div>
  );
}