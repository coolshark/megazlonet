using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogModule.Models.Metadata {

    public class UserProfileMetadata : MvcExtensions.ModelMetadataConfiguration<UserProfile> {

        public UserProfileMetadata() {
            Configure(x => x.UserId)
            .DisplayName("Old password")
            .Required("Old password is required");
        }
    }
}