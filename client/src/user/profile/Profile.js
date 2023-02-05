import React, { Component } from "react";
import "./Profile.css";
import SavedTitle from "./SavedTitle";
import {Link} from 'react-router-dom'
import AddTitle from "../AddTitles/AddTitle";

export default function Profile(props){
  
  console.log(props)
  return (
    <div className="container">
         <div className="main-body">
           <div className="row gutters-sm">
             <div className="col-md-4 mb-3">
               <div className="card card-body d-flex flex-column align-items-center text-center">
                 {props.currentUser.name}
                 <img
                  src={props.currentUser.imageUrl}
                  alt={props.currentUser.name}
                  referrerPolicy="no-referrer"
                  className="rounded-circle"
                  width="150"
                />
              </div>
              {props.savedTitles? <ul>
                {props.savedTitles.map(t=>(
                  <SavedTitle
                  title={t.title}
                  url={t.url}
                  image={t.image}
                  lastUpdated = {t.lastUpdated}></SavedTitle>
                ))}
              </ul>: <h1>You do not have any saved titles! Add some Triggers now!</h1>}
              <button ><span><Link to="/addTitle">Add Titles</Link></span></button>
            </div>
          </div>
        </div>
      </div>
  )
}

// class Profile extends Component {
//   constructor(props) {
//     super(props);

//     this.state = {
//       loading: false,
//     };
//   }

//   async componentDidMount() {
//     await this.loadCurrentlyLoggedInUserDetails();
//   }

//   async loadCurrentlyLoggedInUserDetails() {
//     try {
//       this.setState({
//         loading: true
//       });

//       this.setState({
//         loading: false
//       });
//     } catch (error) {
//       this.setState({
//         loading: false
//       });
//     }
//   }

//   render() {
//     return (
//       <div class="container">
//         <div class="main-body">
//           <div class="row gutters-sm">
//             <div class="col-md-4 mb-3">
//               <div class="card card-body d-flex flex-column align-items-center text-center">
//                 {this.props.currentUser.name}
//                 <img
//                   src={this.props.currentUser.imageUrl}
//                   alt={this.props.currentUser.name}
//                   class="rounded-circle"
//                   width="150"
//                 />
//                 <ul>
//                   {this.props.savedTitles.map(t=>(
//                     <SavedTitle
//                     title={t.title}
//                     path={t.url}
//                     src={t.image}
//                     date = {t.lastUpdated}></SavedTitle>
//                   ))}
//                 </ul>
//               </div>
//             </div>
//           </div>
//         </div>
//       </div>
//     );
//   }
// }

// export default Profile;
