import { API_BASE_URL, ACCESS_TOKEN } from "../constants";

export const request = options => {
  const headers = new Headers({
    "Content-Type": "application/json"
  });

  if (localStorage.getItem(ACCESS_TOKEN)) {
    headers.append("Authorization", "Bearer " + localStorage.getItem(ACCESS_TOKEN));
  }

  const defaults = { headers: headers };
  options = Object.assign({}, defaults, options);

  return fetch(options.url, options).then(response =>
    response.json().then(json => {
      if (!response.ok) {
        return Promise.reject(json);
      }
      return json;
    })
  );
};

export function getCurrentUser() {
  if (!localStorage.getItem(ACCESS_TOKEN)) {
    return Promise.reject("No access token set.");
  }

  return request({
    url: API_BASE_URL + "/profile",
    method: "GET"
  });
}


export function loadSavedTitles(){
  return request({
    url:API_BASE_URL+ '/getAll',
    method:'GET'
  })
}

export function searchTitle(searchTerms){
  const searchParams = new URLSearchParams();
  searchParams.append('title', searchTerms.searchTitle)
  searchParams.append('site', searchTerms.searchSite)

  return request({
    url: API_BASE_URL+'/search?' + searchParams.toString(),
    method:'GET'
  })
}
