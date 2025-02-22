export interface Member {
    id: number
    userName: string
    knownAs: string
    age: number
    photoUrl: string
    photos: Photo[]
    introduction: string
    lookingFor:string
    interests:string
  }
  
  export interface Photo {
    id: number
    url: string
    isMain: boolean
  }
  