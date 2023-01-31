import React from "react";
import { Route, Redirect } from "react-router-dom";
import Profile from "../user/profile/Profile";

// export default function PrivateRoute(props){
//   // console.log(props)
//   const authenticated= props.authenticated
//   return (
//     <>
//     { authenticated? <Profile profile={props}/>: 
//         <Redirect
//               to={{
//                 pathname: "/login",
//                 state: { from: props.location }
//               }}
//             />
//     }
//     </>
//   )
// }
const PrivateRoute = ({ component: Component, authenticated, ...rest }) => (
  <Route
    {...rest}
    render={props =>
      authenticated ? (
        <Component {...rest} {...props} />
      ) : (
        <Redirect
          to={{
            pathname: "/login",
            state: { from: props.location }
          }}
        />
      )
    }
  />
);

export default PrivateRoute;
